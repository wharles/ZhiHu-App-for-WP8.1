#pragma once
namespace BackgroundTasksCX
{
	ref class DataModel sealed
	{
	public:
		DataModel();

		property int Id;

		property Platform::String^ Thumbnail;

		property Platform::String^ Title;
	};

}