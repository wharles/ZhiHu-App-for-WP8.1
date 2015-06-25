// Class1.cpp
#include "pch.h"
#include "ZhiHuAppRuntime.h"

using namespace ZhiHuAppRuntime;
using namespace Platform;
using namespace Platform::Collections;
using namespace std;
using namespace Windows::Foundation::Collections;

RuntimeUrl::RuntimeUrl()
{
}

String^ RuntimeUrl::GetUrl(IVector<String^>^ args)
{
	vector<wstring> para;
	for each (String^ item in args)
	{
		para.push_back(item->Data());
	}
	return ref new String(urls->GetUrl(para).c_str());
}
