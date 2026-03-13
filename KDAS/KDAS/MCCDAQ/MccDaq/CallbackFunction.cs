using System;

namespace MccDaq
{
	public delegate void CallbackFunction(int BoardNum, EventType EventType, int EventData, IntPtr pUserData);
}
