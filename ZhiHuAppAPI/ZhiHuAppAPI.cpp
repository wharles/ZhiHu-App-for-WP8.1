// ZhiHuAppAPI.cpp : Defines the exported functions for the DLL application.
//

#include "pch.h"
#include "ZhiHuAppAPI.h"

using namespace std;

wstring APIUrl::GetUrl(vector<wstring> args)
{
	wstring para = L"";
	for each (wstring arg in args)
	{
		para += (L"/" + arg);
	}
	return rootUrl + para;
}
