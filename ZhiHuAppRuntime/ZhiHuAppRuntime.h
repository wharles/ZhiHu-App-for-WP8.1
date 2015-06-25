#pragma once

#include "../ZhiHuAppAPI/ZhiHuAppAPI.h"

namespace ZhiHuAppRuntime
{
	public ref class RuntimeUrl sealed
    {
	private:
		APIUrl* urls = new APIUrl();
    public:
		RuntimeUrl();
		Platform::String^ GetUrl(Windows::Foundation::Collections::IVector<Platform::String^>^ args);
    };
}