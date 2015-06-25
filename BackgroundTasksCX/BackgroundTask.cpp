// Class1.cpp
#include "pch.h"
#include "BackgroundTask.h"

using namespace BackgroundTasksCX;
using namespace Platform;
using namespace Windows::ApplicationModel::Background;
using namespace Windows::Data::Xml::Dom;
using namespace Windows::UI::Notifications;
using namespace concurrency;
using namespace Windows::Web::Http;
using namespace Windows::Foundation;
using namespace Windows::Data::Json;
using namespace Platform::Collections;

BackgroundTask::BackgroundTask()
{
}

void BackgroundTask::Run(IBackgroundTaskInstance^ taskInstance)
{
	// Get a deferral, to prevent the task from closing prematurely 
	// while asynchronous code is still running.
	BackgroundTaskDeferral^ deferral = taskInstance->GetDeferral();

	create_task(GetDataRandom()).then([deferral, this](DataModel^ model)
	{
		UpdateTile(model);
		deferral->Complete();
	});

	// Inform the system that the task is finished.
}

void BackgroundTask::UpdateTile(DataModel^ model)
{
	// Create a tile update manager for the specified syndication feed.
	auto updater = TileUpdateManager::CreateTileUpdaterForApplication();
	updater->EnableNotificationQueue(true);
	updater->Clear();

	//Update Square Tile
	XmlDocument^ tileXml = TileUpdateManager::GetTemplateContent(TileTemplateType::TileSquarePeekImageAndText04);
	XmlDocument^ wideTileXml = TileUpdateManager::GetTemplateContent(TileTemplateType::TileWidePeekImageAndText01);

	auto tileImageAttributes = tileXml->GetElementsByTagName("image");
	static_cast<XmlElement^>(tileImageAttributes->Item(0))->SetAttribute("src", model->Thumbnail);
	auto tileTextAttributes = tileXml->GetElementsByTagName("text");
	tileTextAttributes->GetAt(0)->InnerText = model->Title;

	//wide tile
	auto wideTileImageAttributes = wideTileXml->GetElementsByTagName("image");
	static_cast<XmlElement^>(wideTileImageAttributes->Item(0))->SetAttribute("src", model->Thumbnail);
	auto wideTileTextAttributes = wideTileXml->GetElementsByTagName("text");
	wideTileTextAttributes->GetAt(0)->InnerText = model->Title;

	// Create a new tile notification. 
	updater->Update(ref new TileNotification(tileXml));
	updater->Update(ref new TileNotification(wideTileXml));

}

task<Platform::String^> BackgroundTask::GetJsonStringAsync(String^ url)
{
	HttpClient^ httpClient = ref new HttpClient();
	HttpRequestMessage^ request = ref new HttpRequestMessage(HttpMethod::Get, ref new Uri(url));
	request->Headers->Append("Accept", "application/json");
	auto response = httpClient->SendRequestAsync(request);
	//return a json string
	return create_task(response).then([](HttpResponseMessage^ response){
		response->EnsureSuccessStatusCode();
		auto responseString = response->Content->ReadAsStringAsync();
		return responseString;
	});
}

task<DataModel^> BackgroundTask::GetDataRandom()
{
	Platform::String^ url = "http://news-at.zhihu.com/api/4/news/latest";
	auto op = GetJsonStringAsync(url);
	return op.then([](String^ result){
		JsonValue^ root = JsonValue::Parse(result);
		JsonObject^ rootObject = root->GetObject();
		Vector<DataModel^>^ models = ref new Vector<DataModel^>();
		if (rootObject->HasKey("top_stories"))
		{
			JsonArray^ dataArray = rootObject->GetNamedArray("top_stories");
			for (int i = 0; i < dataArray->Size; i++)
			{
				DataModel^ data = ref new DataModel();
				data->Id = dataArray->GetAt(i)->GetObject()->GetNamedNumber("id");
				data->Thumbnail = dataArray->GetAt(i)->GetObject()->GetNamedString("image");
				data->Title = dataArray->GetAt(i)->GetObject()->GetNamedString("title");
				models->Append(data);
			}
		}

		srand((unsigned)time(NULL));
		int r = rand() % (models->Size);
		return models->GetAt(r);
	});
}