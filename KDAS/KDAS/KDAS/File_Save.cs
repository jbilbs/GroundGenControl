// Decompiled with JetBrains decompiler
// Type: KDAS.File_Save
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using System;
using System.IO;

namespace KDAS
{
  internal class File_Save
  {
    private static string folderName = "C:\\WMI\\KDAS\\LOGS\\";
    private static string usbFolderName = "\\WMI\\KDAS\\LOGS\\";
    private DateTime currentDate;
    private int currentYear;
    private int currentMonth;
    private int currentDay;
    private int currentHour;
    private int currentMin;
    private int currentSec;
    private string wmifileName;
    private string wmiFileNameUSB;
    private string csvfileName;
    private string csvfileNameUSB;
    private string tailNumber;
    private string usbDriveLetter;
    private string startTime;
    private string startDate;

    public File_Save()
    {
      this.currentYear = 0;
      this.currentMonth = 0;
      this.currentDay = 0;
      this.currentHour = 0;
      this.currentMin = 0;
      this.currentSec = 0;
      this.wmifileName = "";
      this.tailNumber = "";
      this.startDate = "";
      this.startTime = "";
      this.getDateTime();
      this.createFiles();
      this.writeCSV();
    }

    private void getDateTime()
    {
      this.currentDate = DateTime.UtcNow;
      this.currentYear = this.currentDate.Year;
      this.currentMonth = this.currentDate.Month;
      this.currentDay = this.currentDate.Day;
      this.currentHour = this.currentDate.Hour;
      this.currentMin = this.currentDate.Minute;
      this.currentSec = this.currentDate.Second;
      this.startTime = this.currentHour.ToString("00") + "h" + this.currentMin.ToString("00") + "m";
      this.startDate = this.currentYear.ToString("0000") + "_" + this.currentMonth.ToString("00") + "_" + this.currentDay.ToString("00");
    }

    private void createFiles()
    {
      this.usbDriveLetter = Settings.Default.USB;
      this.tailNumber = Settings.Default.TailNumber;
      this.wmifileName = File_Save.folderName + this.tailNumber + "--" + this.startDate + "--" + this.startTime + ".txt";
      this.csvfileName = File_Save.folderName + this.tailNumber + "--" + this.startDate + "--" + this.startTime + ".csv";
      this.wmiFileNameUSB = this.usbDriveLetter + File_Save.usbFolderName + this.tailNumber + "--" + this.startDate + "--" + this.startTime + ".txt";
      this.csvfileNameUSB = this.usbDriveLetter + File_Save.usbFolderName + this.tailNumber + "--" + this.startDate + "--" + this.startTime + ".csv";
      if (!Directory.Exists(File_Save.folderName))
        Directory.CreateDirectory(File_Save.folderName);
      try
      {
        if (Directory.Exists(this.usbDriveLetter + File_Save.usbFolderName))
          return;
        Directory.CreateDirectory(this.usbDriveLetter + File_Save.usbFolderName);
      }
      catch
      {
      }
    }

    public void writeCSV()
    {
      TextWriter textWriter = (TextWriter) new StreamWriter(this.csvfileName, true);
      textWriter.WriteLine("Tail #,Date,Time(UTC),Fix,Latitude(DD),Longitude(DD),MagneticVar,Altitude(decameters),Altitude(feet),Ground Speed(m/s),Ground Speed (knots),BIP Count,BIP State,EJ Count,EJ State,RB State,LB State,ICE State,Liquid Water (g/m^3), Temperature (C)\r\n");
      textWriter.Close();
      try
      {
        textWriter = (TextWriter) new StreamWriter(this.csvfileNameUSB, true);
        textWriter.WriteLine("Tail #,Date,Time(UTC),Fix,Latitude(DD),Longitude(DD),MagneticVar,Altitude(decameters),Altitude(feet),Ground Speed(m/s),Ground Speed (knots),BIP Count,BIP State,EJ Count,EJ State,RB State,LB State,ICE State,Liquid Water (g/m^3), Temperature (C)\r\n");
      }
      catch
      {
      }
      textWriter.Close();
    }

    public void saveCSV(Mission_Snapshot click)
    {
      TextWriter textWriter1 = (TextWriter) new StreamWriter(this.csvfileName, true);
      textWriter1.WriteLine(click.currentCSVstring);
      textWriter1.Close();
      try
      {
        TextWriter textWriter2 = (TextWriter) new StreamWriter(this.csvfileNameUSB, true);
        textWriter2.WriteLine(click.currentCSVstring);
        textWriter2.Close();
      }
      catch
      {
      }
    }

    public void saveWMI()
    {
      TextWriter textWriter = (TextWriter) new StreamWriter(this.wmifileName, true);
      textWriter.WriteLine("DO ME");
      textWriter.Close();
    }

    public void saveWMI(Mission_Snapshot click)
    {
      TextWriter textWriter1 = (TextWriter) new StreamWriter(this.wmifileName, true);
      textWriter1.WriteLine(click.currentWMIstring);
      textWriter1.Close();
      try
      {
        TextWriter textWriter2 = (TextWriter) new StreamWriter(this.wmiFileNameUSB, true);
        textWriter2.WriteLine(click.currentWMIstring);
        textWriter2.Close();
      }
      catch
      {
      }
    }
  }
}
