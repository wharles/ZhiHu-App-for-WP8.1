#pragma once

extern "C++" class __declspec(dllexport) APIUrl
{
public:
	APIUrl();
	~APIUrl();
	std::wstring GetUrl(std::vector<std::wstring> args);
private:
	const std::wstring rootUrl = L"http://news-at.zhihu.com/api";
};

APIUrl::APIUrl()
{
}

APIUrl::~APIUrl()
{
}