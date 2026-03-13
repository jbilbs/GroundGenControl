using System;

namespace MccDaq
{
	[CLSCompliant(false)]
	public delegate void EventCallback(int BoardNum, EventType EventType, uint EventData, IntPtr pUserData);
}
