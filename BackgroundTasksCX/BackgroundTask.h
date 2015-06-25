#pragma once

#include "DataModel.h"

namespace WAB = Windows::ApplicationModel::Background;

namespace BackgroundTasksCX
{
	public ref class BackgroundTask sealed : WAB::IBackgroundTask
	{
	private:
		void UpdateTile(DataModel^ model);
		concurrency::task<Platform::String^> GetJsonStringAsync(Platform::String^ url);
		concurrency::task<DataModel^> GetDataRandom();
	public:
		BackgroundTask();
		virtual void Run(WAB::IBackgroundTaskInstance^ taskInstance);
	};
}