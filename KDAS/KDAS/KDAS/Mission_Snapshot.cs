// Decompiled with JetBrains decompiler
// Type: KDAS.Mission_Snapshot
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;

namespace KDAS
{
  internal class Mission_Snapshot
  {
    private string _currentWMIstring;
    private string _currentCSVstring;
    private int _E1;
    private int _E2;
    private int _E3;
    private int _E4;
    private int _E5;
    private int _E6;
    private int _E7;
    private int _E8;
    private bool _S1;
    private bool _S2;
    private bool _S3;
    private bool _S4;
    private bool _S5;
    private bool _S6;
    private bool _S7;
    private bool _S8;
    private double _A1U;
    private double _A2U;
    private double _A1R;
    private double _A2R;
    private double _A3R;
    private double _A1V;
    private double _A2V;
    private string _rawLat;
    private string _lat;
    private string _rawLon;
    private string _lon;
    private string _rawAlt;
    private string _alt;
    private string _altFeet;
    private string _rawSpeed;
    private string _speed;
    private string _rawSat;
    private string _sat;
    private string _rawFix;
    private string _fix;
    private string _rawMag;
    private string _mag;
    private string _rawPosEr;
    private string _posEr;
    private string _rawTime;
    private string _time;
    private string _rawDate;
    private string _date;
    private bool _ready;
    private bool _gps;
    private bool _write;
    private bool _activity;
    private int _totalSeconds;
    private string _missionDate;
    private string _missionTime;
    private bool _shutdownEvent;
    private int _rbSeconds;
    private int _lbSeconds;
    private int _iceSeconds;
    private int _goodStrings;
    private int _badStrings;
    private string _tailNumber;
    private string _tempA;
    private string _lwA;
    private string _tempF;
    private string _lwF;
    private string _latF;
    private string _lonF;
    private string _spdF;

    public Mission_Snapshot()
    {
      this._currentWMIstring = "";
      this._E1 = 0;
      this._E2 = 0;
      this._E3 = 0;
      this._E4 = 0;
      this._E5 = 0;
      this._E6 = 0;
      this._E7 = 0;
      this._E8 = 0;
      this._S1 = false;
      this._S2 = false;
      this._S3 = false;
      this._S4 = false;
      this._S5 = false;
      this._S6 = false;
      this._S7 = false;
      this._S8 = false;
      this._A1U = 0.0;
      this._A1R = 0.0;
      this._A1V = 0.0;
      this._A2U = 0.0;
      this._A2R = 0.0;
      this._A2V = 0.0;
      this._A3R = 2050.0;
      this._lat = "+000000";
      this._lon = "+0000000";
      this._alt = "0000";
      this._speed = "000";
      this._sat = "0";
      this._fix = "0";
      this._mag = "+0000";
      this._posEr = "000";
      this._time = "00:00:00";
      this._date = "";
      this._rawLat = "0";
      this._rawLon = "0";
      this._rawAlt = "0";
      this._altFeet = "0";
      this._rawSpeed = "0";
      this._rawSat = "0";
      this._rawFix = "0";
      this._rawMag = "0";
      this._rawPosEr = "0";
      this._rawTime = "0";
      this._rawDate = "0";
      this._ready = false;
      this._gps = false;
      this._write = false;
      this._activity = false;
      this._totalSeconds = 0;
      this._missionDate = "????";
      this._missionTime = "????";
      this._shutdownEvent = false;
      this._rbSeconds = 0;
      this._lbSeconds = 0;
      this._iceSeconds = 0;
      this._goodStrings = 0;
      this._badStrings = 0;
      this._currentCSVstring = "";
      this._tempA = "+000";
      this._lwA = "000";
      this._tempF = "0.00";
      this._lwF = "0.00";
      this._tailNumber = Settings.Default.TailNumber;
      this._latF = "";
      this._lonF = "";
      this._spdF = "";
    }

    public string tailNumber
    {
      get
      {
        return this._tailNumber;
      }
      set
      {
        this._tailNumber = value;
      }
    }

    public string currentWMIstring
    {
      get
      {
        return this._currentWMIstring;
      }
      set
      {
        this._currentWMIstring = value;
      }
    }

    public string currentCSVstring
    {
      get
      {
        return this._currentCSVstring;
      }
      set
      {
        this._currentCSVstring = value;
      }
    }

    public int E1
    {
      get
      {
        return this._E1;
      }
      set
      {
        this._E1 = value;
      }
    }

    public int E2
    {
      get
      {
        return this._E2;
      }
      set
      {
        this._E2 = value;
      }
    }

    public int E3
    {
      get
      {
        return this._E3;
      }
      set
      {
        this._E3 = value;
      }
    }

    public int E4
    {
      get
      {
        return this._E4;
      }
      set
      {
        this._E4 = value;
      }
    }

    public int E5
    {
      get
      {
        return this._E5;
      }
      set
      {
        this._E5 = value;
      }
    }

    public int E6
    {
      get
      {
        return this._E6;
      }
      set
      {
        this._E6 = value;
      }
    }

    public int E7
    {
      get
      {
        return this._E7;
      }
      set
      {
        this._E7 = value;
      }
    }

    public int E8
    {
      get
      {
        return this._E8;
      }
      set
      {
        this._E8 = value;
      }
    }

    public bool S1
    {
      get
      {
        return this._S1;
      }
      set
      {
        this._S1 = value;
      }
    }

    public bool S2
    {
      get
      {
        return this._S2;
      }
      set
      {
        this._S2 = value;
      }
    }

    public bool S3
    {
      get
      {
        return this._S3;
      }
      set
      {
        this._S3 = value;
      }
    }

    public bool S4
    {
      get
      {
        return this._S4;
      }
      set
      {
        this._S4 = value;
      }
    }

    public bool S5
    {
      get
      {
        return this._S5;
      }
      set
      {
        this._S5 = value;
      }
    }

    public bool S6
    {
      get
      {
        return this._S6;
      }
      set
      {
        this._S6 = value;
      }
    }

    public bool S7
    {
      get
      {
        return this._S7;
      }
      set
      {
        this._S7 = value;
      }
    }

    public bool S8
    {
      get
      {
        return this._S8;
      }
      set
      {
        this._S8 = value;
      }
    }

    public double A1U
    {
      get
      {
        return this._A1U;
      }
      set
      {
        this._A1U = value;
      }
    }

    public double A2U
    {
      get
      {
        return this._A2U;
      }
      set
      {
        this._A2U = value;
      }
    }

    public double A1R
    {
      get
      {
        return this._A1R;
      }
      set
      {
        this._A1R = value;
      }
    }

    public double A2R
    {
      get
      {
        return this._A2R;
      }
      set
      {
        this._A2R = value;
      }
    }

    public double A3R
    {
      get
      {
        return this._A3R;
      }
      set
      {
        this._A3R = value;
      }
    }

    public double A1V
    {
      get
      {
        return this._A1V;
      }
      set
      {
        this._A1V = value;
      }
    }

    public double A2V
    {
      get
      {
        return this._A2V;
      }
      set
      {
        this._A2V = value;
      }
    }

    public string lat
    {
      get
      {
        return this._lat;
      }
      set
      {
        this._lat = value;
      }
    }

    public string lon
    {
      get
      {
        return this._lon;
      }
      set
      {
        this._lon = value;
      }
    }

    public string alt
    {
      get
      {
        return this._alt;
      }
      set
      {
        this._alt = value;
      }
    }

    public string speed
    {
      get
      {
        return this._speed;
      }
      set
      {
        this._speed = value;
      }
    }

    public string sat
    {
      get
      {
        return this._sat;
      }
      set
      {
        this._sat = value;
      }
    }

    public string fix
    {
      get
      {
        return this._fix;
      }
      set
      {
        this._fix = value;
      }
    }

    public string mag
    {
      get
      {
        return this._mag;
      }
      set
      {
        this._mag = value;
      }
    }

    public string posEr
    {
      get
      {
        return this._posEr;
      }
      set
      {
        this._posEr = value;
      }
    }

    public string time
    {
      get
      {
        return this._time;
      }
      set
      {
        this._time = value;
      }
    }

    public string date
    {
      get
      {
        return this._date;
      }
      set
      {
        this._date = value;
      }
    }

    public string rawLat
    {
      get
      {
        return this._rawLat;
      }
      set
      {
        this._rawLat = value;
      }
    }

    public string rawLon
    {
      get
      {
        return this._rawLon;
      }
      set
      {
        this._rawLon = value;
      }
    }

    public string rawAlt
    {
      get
      {
        return this._rawAlt;
      }
      set
      {
        this._rawAlt = value;
      }
    }

    public string rawAltFeet
    {
      get
      {
        return this._altFeet;
      }
      set
      {
        this._altFeet = value;
      }
    }

    public string rawSpeed
    {
      get
      {
        return this._rawSpeed;
      }
      set
      {
        this._rawSpeed = value;
      }
    }

    public string rawSat
    {
      get
      {
        return this._rawSat;
      }
      set
      {
        this._rawSat = value;
      }
    }

    public string rawFix
    {
      get
      {
        return this._rawFix;
      }
      set
      {
        this._rawFix = value;
      }
    }

    public string rawMag
    {
      get
      {
        return this._rawMag;
      }
      set
      {
        this._rawMag = value;
      }
    }

    public string rawPosEr
    {
      get
      {
        return this._rawPosEr;
      }
      set
      {
        this._rawPosEr = value;
      }
    }

    public string rawTime
    {
      get
      {
        return this._rawTime;
      }
      set
      {
        this._rawTime = value;
      }
    }

    public string rawDate
    {
      get
      {
        return this._rawDate;
      }
      set
      {
        this._rawDate = value;
      }
    }

    public bool ready
    {
      get
      {
        return this._ready;
      }
      set
      {
        this._ready = value;
      }
    }

    public bool gps
    {
      get
      {
        return this._gps;
      }
      set
      {
        this._gps = value;
      }
    }

    public bool write
    {
      get
      {
        return this._write;
      }
      set
      {
        this._write = value;
      }
    }

    public bool activity
    {
      get
      {
        return this._activity;
      }
      set
      {
        this._activity = value;
      }
    }

    public int totalSeconds
    {
      get
      {
        return this._totalSeconds;
      }
      set
      {
        this._totalSeconds = value;
      }
    }

    public string missionDate
    {
      get
      {
        return this._missionDate;
      }
      set
      {
        this._missionDate = value;
      }
    }

    public string missionTime
    {
      get
      {
        return this._missionTime;
      }
      set
      {
        this._missionTime = value;
      }
    }

    public int rbSeconds
    {
      get
      {
        return this._rbSeconds;
      }
      set
      {
        this._rbSeconds = value;
      }
    }

    public int lbSeconds
    {
      get
      {
        return this._lbSeconds;
      }
      set
      {
        this._lbSeconds = value;
      }
    }

    public int iceSeconds
    {
      get
      {
        return this._iceSeconds;
      }
      set
      {
        this._iceSeconds = value;
      }
    }

    public int goodSeconds
    {
      get
      {
        return this._goodStrings;
      }
      set
      {
        this._goodStrings = value;
      }
    }

    public int badSeconds
    {
      get
      {
        return this._badStrings;
      }
      set
      {
        this._badStrings = value;
      }
    }

    public string tempA
    {
      get
      {
        return this._tempA;
      }
      set
      {
        this._tempA = value;
      }
    }

    public string lwA
    {
      get
      {
        return this._lwA;
      }
      set
      {
        this._lwA = value;
      }
    }

    public string tempF
    {
      get
      {
        return this._tempF;
      }
      set
      {
        this._tempF = value;
      }
    }

    public string lwF
    {
      get
      {
        return this._lwF;
      }
      set
      {
        this._lwF = value;
      }
    }

    public string spdF
    {
      get
      {
        return this._spdF;
      }
      set
      {
        this._spdF = value;
      }
    }

    public string latF
    {
      get
      {
        return this._latF;
      }
      set
      {
        this._latF = value;
      }
    }

    public string lonF
    {
      get
      {
        return this._lonF;
      }
      set
      {
        this._lonF = value;
      }
    }

    public bool shutdownEvent
    {
      get
      {
        return this._shutdownEvent;
      }
      set
      {
        this._shutdownEvent = value;
      }
    }
  }
}
