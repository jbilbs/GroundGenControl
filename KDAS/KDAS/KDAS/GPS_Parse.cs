// Decompiled with JetBrains decompiler
// Type: KDAS.GPS_Parse
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using System;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace KDAS
{
  internal class GPS_Parse
  {
    private const string GPRMC = "$GPRMC";
    private const string GPGGA = "$GPGGA";
    private const string GPZDA = "$GPZDA";
    private SerialPort GPSPort;
    private int _latitude_Start;
    private int _latitude_End;
    private int _longitude_Start;
    private int _longitude_End;
    private int _latitudeDir_Start;
    private int _latitudeDir_End;
    private int _longitudeDir_Start;
    private int _longitudeDir_End;
    private int _magneticVariation_Start;
    private int _magneticVariation_End;
    private int _magneticVariationDir_Start;
    private int _magneticVariationDir_End;
    private int _gpsFix_Start;
    private int _gpsFix_End;
    private int _altitude_Start;
    private int _altitude_End;
    private int _horizontalError_Start;
    private int _horizontalError_End;
    private int _groundSpeed_Start;
    private int _groundSpeed_End;
    private int _satellites_Start;
    private int _satellites_End;
    private int _time_Start;
    private int _time_End;
    private int _day_Start;
    private int _day_End;
    private int _month_Start;
    private int _month_End;
    private int _year_Start;
    private int _year_End;
    private string _rawLatitude;
    private string _latitude;
    private string _rawLongitude;
    private string _longitude;
    private string _rawMagneticVariation;
    private string _magneticVariation;
    private string _rawAltitude;
    private string _altitude;
    private string _altitudeFeet;
    private string _rawGroundSpeed;
    private string _groundSpeed;
    private string _rawTime;
    private string _time;
    private string _rawDate;
    private string _date;
    private string _rawSatellites;
    private string _satellites;
    private string _rawFix;
    private string _fix;
    private string _rawHorizontalError;
    private string _horizontalError;
    private string header;
    private string data;
    private string trimmedData;
    private bool _timeSyncReady;
    private bool _timeSynced;
    private string _lonF;
    private string _latF;
    private string _spdF;

    [DllImport("kernel32.dll")]
    private static extern void GetSystemTime(ref GPS_Parse.SYSTEMTIME lpSystemTime);

    [DllImport("kernel32.dll")]
    private static extern bool SetSystemTime(ref GPS_Parse.SYSTEMTIME time);

    public GPS_Parse()
    {
      this.GPSPort = new SerialPort();
      this._latitude_Start = 0;
      this._latitude_End = 0;
      this._longitude_Start = 0;
      this._longitude_End = 0;
      this._latitudeDir_Start = 0;
      this._latitudeDir_End = 0;
      this._longitudeDir_Start = 0;
      this._longitudeDir_End = 0;
      this._magneticVariation_Start = 0;
      this._magneticVariation_End = 0;
      this._magneticVariationDir_Start = 0;
      this._magneticVariationDir_End = 0;
      this._gpsFix_Start = 0;
      this._gpsFix_End = 0;
      this._altitude_Start = 0;
      this._altitude_End = 0;
      this._horizontalError_Start = 0;
      this._horizontalError_End = 0;
      this._groundSpeed_Start = 0;
      this._groundSpeed_End = 0;
      this._satellites_Start = 0;
      this._satellites_End = 0;
      this._time_Start = 0;
      this._time_End = 0;
      this._day_Start = 0;
      this._day_End = 0;
      this._month_Start = 0;
      this._month_End = 0;
      this._year_Start = 0;
      this._year_End = 0;
      this._rawLatitude = "+000000";
      this._latitude = "+000000";
      this._rawLongitude = "-0000000";
      this._longitude = "-0000000";
      this._rawMagneticVariation = "+0000";
      this._magneticVariation = "+0000";
      this._rawAltitude = "0000";
      this._altitude = "0000";
      this._altitudeFeet = "0000";
      this._rawGroundSpeed = "000";
      this._groundSpeed = "000";
      this._time = "00:00:00";
      this._rawTime = "000000";
      this._date = "00/00/00";
      this._rawDate = "000000";
      this._rawHorizontalError = "999";
      this._horizontalError = "999";
      this._rawFix = "0";
      this._fix = "0";
      this._rawSatellites = "0";
      this._satellites = "0";
      this._timeSyncReady = false;
      this._timeSynced = false;
      this._latF = "";
      this._lonF = "";
      this._spdF = "";
      this.header = "";
      this.data = "";
      this.trimmedData = "";
      this.GPS_Init();
    }

    public void closeGPSport()
    {
      this.GPSPort.Close();
    }

    private void GPS_Init()
    {
      this.GPSPort.PortName = Settings.Default.GPSport;
      this.GPSPort.BaudRate = 9600;
      this.GPSPort.Parity = Parity.None;
      this.GPSPort.StopBits = StopBits.One;
      this.GPSPort.DataBits = 8;
      this.GPSPort.Handshake = Handshake.None;
      this.GPSPort.ReadTimeout = 50000;
      this.GPSPort.WriteTimeout = 50000;
      this.GPSPort.DataReceived += new SerialDataReceivedEventHandler(this.DataReceived);
      this.GPSPort.Close();
      this.GPSPort.Open();
    }

    private void DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      this.data = this.GPSPort.ReadLine();
      this.trimmedData = this.data.Trim();
      try
      {
        this.header = this.data.Substring(0, 6);
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
      if (this.header == "$GPRMC")
      {
        int num1 = 0;
        int startIndex = 0;
        string str1 = "";
        int num2;
        while ((num2 = this.trimmedData.IndexOf(',', startIndex)) != -1)
        {
          startIndex = num2 + 1;
          ++num1;
          switch (num1)
          {
            case 3:
              this._latitude_Start = startIndex;
              continue;
            case 4:
              this._latitude_End = startIndex - 1;
              this._latitudeDir_Start = startIndex;
              continue;
            case 5:
              this._latitudeDir_End = startIndex - 1;
              this._longitude_Start = startIndex;
              continue;
            case 6:
              this._longitude_End = startIndex - 1;
              this._longitudeDir_Start = startIndex;
              continue;
            case 7:
              this._longitudeDir_End = startIndex - 1;
              this._groundSpeed_Start = startIndex;
              continue;
            case 8:
              this._groundSpeed_End = startIndex - 1;
              continue;
            case 10:
              this._magneticVariation_Start = startIndex;
              continue;
            case 11:
              this._magneticVariationDir_Start = startIndex;
              this._magneticVariation_End = startIndex - 1;
              continue;
            case 12:
              this._magneticVariationDir_End = startIndex - 1;
              continue;
            default:
              continue;
          }
        }
        string str2 = this.trimmedData.Substring(this._latitude_Start, this._latitude_End - this._latitude_Start);
        string str3 = this.trimmedData.Substring(this._latitudeDir_Start, this._latitudeDir_End - this._latitudeDir_Start);
        string str4 = this.trimmedData.Substring(this._longitude_Start, this._longitude_End - this._longitude_Start);
        string str5 = this.trimmedData.Substring(this._longitudeDir_Start, this._longitudeDir_End - this._longitudeDir_Start);
        string str6 = this.trimmedData.Substring(this._groundSpeed_Start, this._groundSpeed_End - this._groundSpeed_Start);
        string str7 = this.trimmedData.Substring(this._magneticVariation_Start, this._magneticVariation_End - this._magneticVariation_Start);
        string str8 = this.trimmedData.Substring(this._magneticVariationDir_Start, this._magneticVariationDir_End - this._magneticVariationDir_Start);
        this._rawLatitude = str2 + " " + str3;
        if (this._rawLatitude == " ")
          this._rawLatitude = "-";
        this._rawLongitude = str4 + " " + str5;
        if (this._rawLongitude == " ")
          this._rawLongitude = "-";
        this._rawGroundSpeed = str6;
        this._rawMagneticVariation = str7 + " " + str8;
        if (!(str3 == "") && !(str2 == ""))
        {
          double num3 = Convert.ToDouble(str2.Substring(0, 2)) + Convert.ToDouble(str2.Substring(2, 7)) / 60.0;
          this._latF = str3 + " " + Convert.ToString(Math.Round(num3, 4));
          string str9 = "00000" + Convert.ToString(num3) + "00000";
          int num4 = str9.IndexOf('.');
          string str10 = str9.Substring(num4 - 2, 2) + str9.Substring(num4 + 1, 4);
          if (str3 == "n" || str3 == "N")
            this._latitude = "+" + str10;
          else if (str3 == "s" || str3 == "S")
            this._latitude = "-" + str10;
        }
        if (!(str5 == "") && !(str4 == ""))
        {
          double num3 = Convert.ToDouble(str4.Substring(0, 3)) + Convert.ToDouble(str4.Substring(3)) / 60.0;
          this._lonF = str5 + " " + Convert.ToString(Math.Round(num3, 4));
          string str9 = "00000" + Convert.ToString(num3) + "00000";
          int num4 = str9.IndexOf('.');
          string str10 = str9.Substring(num4 - 3, 3) + str9.Substring(num4 + 1, 4);
          if (str5 == nameof (e) || str5 == "E")
            this._longitude = "+" + str10;
          else if (str5 == "w" || str5 == "W")
            this._longitude = "-" + str10;
        }
        if (!(str6 == ""))
        {
          double single = (double) Convert.ToSingle(str6);
          this._spdF = Convert.ToString((int) Math.Round(single));
          string str9 = Convert.ToString((double) (int) Math.Round(single / 0.544439971446991));
          if (str9.Length == 1)
            this._groundSpeed = "00" + str9;
          else if (str9.Length == 2)
            this._groundSpeed = "0" + str9;
          else if (str9.Length == 3)
            this._groundSpeed = str9;
        }
        if (str7 == "" || str8 == "")
          return;
        if (str7.Length == 3)
          str1 = "0" + str7.Substring(0, 1) + str7.Substring(2, 1) + "0";
        else if (str7.Length == 4)
          str1 = str7.Substring(0, 2) + str7.Substring(3, 1) + "0";
        if (str8 == nameof (e) || str8 == "E")
        {
          this._magneticVariation = "+" + str1;
        }
        else
        {
          if (!(str8 == "w") && !(str8 == "W"))
            return;
          this._magneticVariation = "-" + str1;
        }
      }
      else if (this.header == "$GPGGA")
      {
        int num1 = 0;
        int startIndex = 0;
        int num2;
        while ((num2 = this.trimmedData.IndexOf(',', startIndex)) != -1)
        {
          ++num1;
          startIndex = num2 + 1;
          switch (num1)
          {
            case 6:
              this._gpsFix_Start = startIndex;
              continue;
            case 7:
              this._gpsFix_End = startIndex - 1;
              this._satellites_Start = startIndex;
              continue;
            case 8:
              this._satellites_End = startIndex - 1;
              this._horizontalError_Start = startIndex;
              continue;
            case 9:
              this._horizontalError_End = startIndex - 1;
              this._altitude_Start = startIndex;
              continue;
            case 10:
              this._altitude_End = startIndex - 1;
              continue;
            default:
              continue;
          }
        }
        this.trimmedData.Substring(this._horizontalError_Start, this._horizontalError_End - this._horizontalError_Start);
        string str1 = this.trimmedData.Substring(this._altitude_Start, this._altitude_End - this._altitude_Start);
        string str2 = this.trimmedData.Substring(this._gpsFix_Start, this._gpsFix_End - this._gpsFix_Start);
        string str3 = this.trimmedData.Substring(this._satellites_Start, this._satellites_End - this._satellites_Start);
        string str4 = this.trimmedData.Substring(this._horizontalError_Start, this._horizontalError_End - this._horizontalError_Start);
        this._rawAltitude = str1;
        if (this._rawAltitude == "")
          this._rawAltitude = "-";
        this._rawFix = str2;
        this._rawSatellites = str3;
        if (this._rawSatellites.Length == 1)
          this._rawSatellites = "0" + this._rawSatellites;
        this._rawHorizontalError = str4;
        if (this._rawHorizontalError == "")
          this._rawHorizontalError = "999";
        if (!(str1 == ""))
        {
          float num3 = (float) (int) Math.Round((double) (Convert.ToSingle(str1) / 10f));
          if ((double) num3 < 0.0)
            num3 = 0.0f;
          string str5 = Convert.ToString(num3);
          float num4 = (float) (int) Math.Round((double) (Convert.ToSingle(str1) * 3.28084f));
          if ((double) num4 < 0.0)
            num4 = 0.0f;
          this._altitudeFeet = Convert.ToString(num4);
          if (str5.Length == 1)
            this._altitude = "000" + str5;
          else if (str5.Length == 2)
            this._altitude = "00" + str5;
          else if (str5.Length == 3)
            this._altitude = "0" + str5;
          else if (str5.Length == 4)
            this._altitude = str5;
        }
        if (str4 == "")
          this._horizontalError = "999";
        else if (str4.Length == 4)
          this._horizontalError = "0" + str4.Substring(0, 1) + str4.Substring(2, 1);
        else if (str4.Length == 5)
          this._horizontalError = str4.Substring(0, 2) + str4.Substring(3, 1);
        this._fix = str2;
        if (!this._timeSynced && (this._fix == "1" || this._fix == "2" || (this._fix == "3" || this._fix == "4") || (this._fix == "5" || this._fix == "6")))
          this._timeSyncReady = true;
        this._satellites = str3;
      }
      else
      {
        if (!(this.header == "$GPZDA"))
          return;
        int num1 = 0;
        int startIndex = 0;
        int num2;
        while ((num2 = this.trimmedData.IndexOf(',', startIndex)) != -1)
        {
          ++num1;
          startIndex = num2 + 1;
          switch (num1)
          {
            case 1:
              this._time_Start = startIndex;
              continue;
            case 2:
              this._time_End = startIndex - 1;
              this._day_Start = startIndex;
              continue;
            case 3:
              this._day_End = startIndex - 1;
              this._month_Start = startIndex;
              continue;
            case 4:
              this._month_End = startIndex - 1;
              this._year_Start = startIndex;
              continue;
            case 5:
              this._year_End = startIndex - 1;
              continue;
            default:
              continue;
          }
        }
        string str1 = this.trimmedData.Substring(this._time_Start, this._time_End - this._time_Start);
        this._rawDate = this.trimmedData.Substring(this._day_Start, this._year_End - this._day_Start);
        this._rawTime = str1;
        string str2 = this.trimmedData.Substring(this._month_Start, this._month_End - this._month_Start);
        string str3 = this.trimmedData.Substring(this._year_Start, this._year_End - this._year_Start);
        string str4 = this.trimmedData.Substring(this._day_Start, this._day_End - this._day_Start);
        this._date = str2 + "/" + str4 + "/" + str3;
        string str5 = str1.Substring(4, 2);
        string str6 = str1.Substring(2, 2);
        string str7 = str1.Substring(0, 2);
        this._time = str7 + ":" + str6 + ":" + str5;
        if (!this._timeSyncReady || this.timeSyced)
          return;
        GPS_Parse.SYSTEMTIME systime = new SYSTEMTIME()
        {
            wDay = Convert.ToUInt16(str4),
            wHour = Convert.ToUInt16(str7),
            wMinute = Convert.ToUInt16(str6),
            wMonth = Convert.ToUInt16(str2),
            wSecond = Convert.ToUInt16(str5),
            wYear = Convert.ToUInt16(str3)
        };
        GPS_Parse.SetSystemTime(ref systime);
        this._timeSynced = true;
      }
    }

    public string latitude
    {
      get
      {
        return this._latitude;
      }
    }

    public string longitude
    {
      get
      {
        return this._longitude;
      }
    }

    public string magneticVariation
    {
      get
      {
        return this._magneticVariation;
      }
    }

    public string fix
    {
      get
      {
        return this._fix;
      }
    }

    public string altitude
    {
      get
      {
        return this._altitude;
      }
    }

    public string groundSpeed
    {
      get
      {
        return this._groundSpeed;
      }
    }

    public string satellites
    {
      get
      {
        return this._satellites;
      }
    }

    public string time
    {
      get
      {
        return this._time;
      }
    }

    public string date
    {
      get
      {
        return this._date;
      }
    }

    public string horizontalError
    {
      get
      {
        return this._horizontalError;
      }
    }

    public string altitudeFeet
    {
      get
      {
        return this._altitudeFeet;
      }
    }

    public string rawLatitude
    {
      get
      {
        return this._rawLatitude;
      }
    }

    public string rawLongitude
    {
      get
      {
        return this._rawLongitude;
      }
    }

    public string rawMagneticVariation
    {
      get
      {
        return this._rawMagneticVariation;
      }
    }

    public string rawFix
    {
      get
      {
        return this._rawFix;
      }
    }

    public string rawAltitude
    {
      get
      {
        return this._rawAltitude;
      }
    }

    public string rawGroundSpeed
    {
      get
      {
        return this._rawGroundSpeed;
      }
    }

    public string rawSatellites
    {
      get
      {
        return this._rawSatellites;
      }
    }

    public string rawTime
    {
      get
      {
        return this._rawTime;
      }
    }

    public string rawDate
    {
      get
      {
        return this._rawDate;
      }
    }

    public string rawHorizontalError
    {
      get
      {
        return this._rawHorizontalError;
      }
    }

    public bool timeSyced
    {
      get
      {
        return this._timeSynced;
      }
    }

    public string lonF
    {
      get
      {
        return this._lonF;
      }
    }

    public string latF
    {
      get
      {
        return this._latF;
      }
    }

    public string spdF
    {
      get
      {
        return this._spdF;
      }
    }

    private struct SYSTEMTIME
    {
      public ushort wYear;
      public ushort wMonth;
      public ushort wDayOfWeek;
      public ushort wDay;
      public ushort wHour;
      public ushort wMinute;
      public ushort wSecond;
      public ushort wMilliseconds;
    }
  }
}
