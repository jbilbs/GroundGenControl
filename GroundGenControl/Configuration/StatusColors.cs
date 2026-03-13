using System;
using System.Drawing;

namespace GroundGenControl.Configuration
{
	/// <summary>
	/// Summary description for StatusColors.
	/// </summary>
	public class StatusColors
	{
		//	Colors reference by connection/communication states
		public static System.Drawing.Color CONNECTED			=	Color.Green;
		public static System.Drawing.Color DISCONNECTED			=	System.Drawing.SystemColors.Control;
		public static System.Drawing.Color SENDING				=	Color.Green;
		public static System.Drawing.Color NOT_SENDING			=	System.Drawing.SystemColors.Control;
		public static System.Drawing.Color RECEIVING			=	Color.Green;
		public static System.Drawing.Color NOT_RECEIVING		=	System.Drawing.SystemColors.Control;
		public static System.Drawing.Color BURNING				=	Color.Green;
		public static System.Drawing.Color NOT_BURNING			=	Color.White;
		public static System.Drawing.Color SEEDING				=	Color.Green;
		public static System.Drawing.Color NOT_SEEDING			=	Color.White;

		public static System.Drawing.Color VALVE_ON				=	Color.Green;
		public static System.Drawing.Color VALVE_OFF			=	Color.LightGray;


		public static System.Drawing.Color PURGE_VALVE_ON		=	Color.Yellow;
		public static System.Drawing.Color PURGE_VALVE_OFF		=	Color.LightGray;

		public static System.Drawing.Color SOLUTION_VALVE_ON	=	Color.DarkBlue;
		public static System.Drawing.Color SOLUTION_VALVE_OFF	=	Color.LightGray;


		public static System.Drawing.Color BATTERY_TEXT_NORMAL		=	Color.White;
		public static System.Drawing.Color BATTERY_TEXT_LOW			=	Color.Red;


		//	Color for all disconnected items
		public static System.Drawing.Color	DISCONNECTED_SYSTEM	=	Color.LightGray;


	}
}
