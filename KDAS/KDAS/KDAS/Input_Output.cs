// Decompiled with JetBrains decompiler
// Type: KDAS.Input_Output
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using KDAS.Properties;
using MccDaq;

namespace KDAS
{
  internal class Input_Output
  {
    private bool reset1 = true;
    private bool reset2 = true;
    private bool reset6 = true;
    private bool reset7 = true;
    private bool reset8 = true;
    private const DigitalPortType PortNum_In = DigitalPortType.FirstPortA;
    private const DigitalPortType PortNum_Out = DigitalPortType.FirstPortB;
    private const DigitalPortDirection Input = DigitalPortDirection.DigitalIn;
    private const DigitalPortDirection Output = DigitalPortDirection.DigitalOut;
    private const Range DaqRange = Range.Bip5Volts;
    public Transfer_Functions TF;
    private MccBoard DaqBoard;
    private ErrorInfo ULStat;
    private int _Event1;
    private int _Event2;
    private int _Event3;
    private int _Event4;
    private int _Event5;
    private int _Event6;
    private int _Event7;
    private int _Event8;
    private bool _State1;
    private bool _State2;
    private bool _State3;
    private bool _State4;
    private bool _State5;
    private bool _State6;
    private bool _State7;
    private bool _State8;
    private short data_In;
    private ushort data_Out;
    private double _AnalogRaw1;
    private double _AnalogVolts1;
    private double _AnalogUnits1;
    private double _AnalogRaw2;
    private double _AnalogVolts2;
    private double _AnalogUnits2;
    private double _AnalogRaw3;
    private short Analog;
    private string _tempA;
    private string _lwA;
    private string _tempF;
    private string _lwF;
    private bool shutdown;
    private double[] test;
    private int spotkeeper;

    public Input_Output()
    {
      this.TF = new Transfer_Functions();
      this.DaqBoard = new MccBoard(0);
            //MccService.WinBufAllocEx(4096);
      this.ULStat = MccService.ErrHandling(ErrorReporting.DontPrint, ErrorHandling.DontStop);
      this.ULStat = this.DaqBoard.DConfigPort(DigitalPortType.FirstPortB, DigitalPortDirection.DigitalOut);
      this.ULStat = this.DaqBoard.DConfigPort(DigitalPortType.FirstPortA, DigitalPortDirection.DigitalIn);
      this.shutdown = Settings.Default.AllowShutdown;
      this._tempF = "00.0";
      this._tempA = "+000";
      this._lwF = "0.00";
      this._lwA = "000";
      this.test = new double[10];
      this.spotkeeper = 0;
      for (int index = 0; index < 10; ++index)
        this.test[index] = 0.0;
    }

    private double getRunningAverage(short raw)
    {
      int num1 = 0;
      double num2 = 0.0;
      this.test[this.spotkeeper] = (double) raw;
      ++this.spotkeeper;
      this.spotkeeper %= 10;
      for (int index = 0; index < 10; ++index)
      {
        if (this.test[index] != 0.0)
        {
          num2 += this.test[index];
          ++num1;
        }
      }
      if (num1 == 0)
        return 0.0;
      return num2 / (double) num1;
    }

    public void ScanPorts()
    {
      this.ULStat = this.DaqBoard.DIn(DigitalPortType.FirstPortA, out this.data_In);
      if (((int) this.data_In & 1) != 0 && this.reset1)
      {
        this._State1 = true;
        ++this._Event1;
        this.reset1 = false;
      }
      if (((int) this.data_In & 2) != 0 && this.reset2)
      {
        this._State2 = true;
        ++this._Event2;
        this.reset2 = false;
      }
      if (((int) this.data_In & 4) != 0)
      {
        this._State3 = true;
        this._Event3 = 1;
      }
      else
      {
        this._State3 = false;
        this._Event3 = 0;
      }
      if (((int) this.data_In & 8) != 0)
      {
        this._State4 = true;
        this._Event4 = 1;
      }
      else
      {
        this._State4 = false;
        this._Event4 = 0;
      }
      if (((int) this.data_In & 16) != 0)
      {
        this._State5 = true;
        this._Event5 = 1;
      }
      else
      {
        this._State5 = false;
        this._Event5 = 0;
      }
      if (((int) this.data_In & 32) != 0 && this.reset6)
      {
        this._State6 = true;
        ++this._Event6;
        this.reset6 = false;
      }
      if (((int) this.data_In & 64) != 0 && this.reset7)
      {
        this._State7 = true;
        ++this._Event7;
        this.reset7 = false;
      }
      if (((int) this.data_In & 128) != 0 && this.reset8 && this.shutdown)
      {
        this._State8 = true;
        ++this._Event8;
        this.reset8 = false;
      }
      this.ULStat = this.DaqBoard.AIn(0, Range.Bip5Volts, out this.Analog);
      this._AnalogRaw1 = this.getRunningAverage(this.Analog);
      this._AnalogVolts1 = this.TF.raw2volt(this._AnalogRaw1);
      this._AnalogUnits1 = this.TF.volt2tempature(this._AnalogVolts1);
      string str = "+";
      if (this._AnalogUnits1 < 0.0)
        str = "";
      this._tempA = this._AnalogUnits1 <= 49.0 ? (this._AnalogUnits1 >= -49.0 ? str + string.Format("{0:000}", (object) (this._AnalogUnits1 * 10.0)) : "-500") : "+500";
      this._tempF = this._AnalogUnits2 <= 75.0 ? (this._AnalogUnits2 >= -50.0 ? (this._AnalogUnits1 >= 0.0 ? "+" : "") + string.Format("{0:00.0}", (object) this._AnalogUnits1) : "-50.0") : "+75.00";
      this.ULStat = this.DaqBoard.AIn(1, Range.Bip5Volts, out this.Analog);
      this._AnalogRaw2 = (double) this.Analog;
      this._AnalogVolts2 = this.TF.raw2volt(this._AnalogRaw2);
      this._AnalogUnits2 = this.TF.volt2liquidWater2(this._AnalogVolts2);
      this._lwA = this._AnalogUnits2 >= -0.99 ? (this._AnalogUnits2 >= 0.0 ? (this._AnalogUnits2 <= 9.99 ? string.Format("{0:000}", (object) (this._AnalogUnits2 * 100.0)) : "999") : string.Format("{0:00}", (object) (this._AnalogUnits2 * 100.0))) : "-99";
      this._lwF = this._AnalogUnits2 <= 9.99 ? (this._AnalogUnits2 >= -9.99 ? (this._AnalogUnits2 >= 0.0 ? "+" : "") + string.Format("{0:0.00}", (object) this._AnalogUnits2) : "-9.99") : "+9.99";
      this.ULStat = this.DaqBoard.AIn(2, Range.Bip5Volts, out this.Analog);
      this._AnalogRaw3 = (double) this.Analog;
    }

    public void setLEDs(bool ready, bool gps, bool write, bool activity)
    {
      this.data_Out = (ushort) 0;
      if (ready)
        ++this.data_Out;
      if (gps)
        this.data_Out += (ushort) 2;
      if (write)
        this.data_Out += (ushort) 4;
      if (activity)
        this.data_Out += (ushort) 8;
      this.ULStat = this.DaqBoard.DOut(DigitalPortType.FirstPortB, this.data_Out);
    }

    public void resetAll()
    {
      this.reset1 = true;
      this.reset2 = true;
      this.reset6 = true;
      this.reset7 = true;
      this.reset8 = true;
      this._State1 = false;
      this._State2 = false;
      this._State6 = false;
      this._State7 = false;
      this._State8 = false;
    }

    public void ledsOff()
    {
      this.data_Out = (ushort) 0;
      this.ULStat = this.DaqBoard.DOut(DigitalPortType.FirstPortB, this.data_Out);
    }

    public bool setActivity()
    {
      return this._State1 || this._State2 || (this._State6 || this._State7) || this._State8;
    }

    public double zeroLW()
    {
      this.TF.trim = this.AnalogVolts2;
      return this.AnalogVolts2;
    }

    public int Event1
    {
      get
      {
        return this._Event1;
      }
    }

    public int Event2
    {
      get
      {
        return this._Event2;
      }
    }

    public int Event3
    {
      get
      {
        return this._Event3;
      }
    }

    public int Event4
    {
      get
      {
        return this._Event4;
      }
    }

    public int Event5
    {
      get
      {
        return this._Event5;
      }
    }

    public int Event6
    {
      get
      {
        return this._Event6;
      }
    }

    public int Event7
    {
      get
      {
        return this._Event7;
      }
    }

    public int Event8
    {
      get
      {
        return this._Event8;
      }
    }

    public bool State1
    {
      get
      {
        return this._State1;
      }
    }

    public bool State2
    {
      get
      {
        return this._State2;
      }
    }

    public bool State3
    {
      get
      {
        return this._State3;
      }
    }

    public bool State4
    {
      get
      {
        return this._State4;
      }
    }

    public bool State5
    {
      get
      {
        return this._State5;
      }
    }

    public bool State6
    {
      get
      {
        return this._State6;
      }
    }

    public bool State7
    {
      get
      {
        return this._State7;
      }
    }

    public bool State8
    {
      get
      {
        return this._State8;
      }
    }

    public double AnalogRaw1
    {
      get
      {
        return this._AnalogRaw1;
      }
    }

    public double AnalogRaw2
    {
      get
      {
        return this._AnalogRaw2;
      }
    }

    public double AnalogRaw3
    {
      get
      {
        return this._AnalogRaw3;
      }
    }

    public double AnalogVolts1
    {
      get
      {
        return this._AnalogVolts1;
      }
    }

    public double AnalogVolts2
    {
      get
      {
        return this._AnalogVolts2;
      }
    }

    public double AnalogUnits1
    {
      get
      {
        return this._AnalogUnits1;
      }
    }

    public double AnalogUnits2
    {
      get
      {
        return this._AnalogUnits2;
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
  }
}
