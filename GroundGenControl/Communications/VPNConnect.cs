using System;
using System.Diagnostics;
using System.IO;

namespace GroundGenControl.Communications
{
	/// <summary>
	/// Summary description for VPNConnect.
	/// </summary>
	public class VPNConnect
	{
		public VPNConnect()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static bool connectTo(String strProfile, String strUserName, String strPassword )
		{

			ProcessStartInfo psi = new ProcessStartInfo("rasdial.exe", strProfile + " " + strUserName + " " + strPassword);
			psi.RedirectStandardOutput	=	true;
			psi.RedirectStandardInput	=	true;
			psi.UseShellExecute			=	false;
			psi.CreateNoWindow			=	true;
			Process proc				=	Process.Start(psi);
			StreamReader reader = proc.StandardOutput;

			String line;

			while ((line = reader.ReadLine())!=null)
			{

				if ( line.IndexOf("Successfully connected to") != -1 )
				{
					return true;
				}
			}

			return false;
		}

		public static void disconnect(String strProfile )
		{

			ProcessStartInfo psi = new ProcessStartInfo("rasdial.exe", strProfile + @" /disconnect");
			psi.RedirectStandardOutput	=	true;
			psi.RedirectStandardInput	=	true;
			psi.UseShellExecute			=	false;
			psi.CreateNoWindow			=	true;
			Process proc				=	Process.Start(psi);
			StreamReader reader = proc.StandardOutput;

			String line;

			while ((line = reader.ReadLine())!=null)
			{
			}
		}

	}
}
