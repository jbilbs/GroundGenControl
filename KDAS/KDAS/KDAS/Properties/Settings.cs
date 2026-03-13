// Decompiled with JetBrains decompiler
// Type: KDAS.Properties.Settings
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace KDAS.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
    {
    }

    private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
    {
    }

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("N1234")]
    [UserScopedSetting]
    public string TailNumber
    {
      get
      {
        return (string) this[nameof (TailNumber)];
      }
      set
      {
        this[nameof (TailNumber)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("1")]
    public string TXtime
    {
      get
      {
        return (string) this[nameof (TXtime)];
      }
      set
      {
        this[nameof (TXtime)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    public bool AllowShutdown
    {
      get
      {
        return (bool) this[nameof (AllowShutdown)];
      }
      set
      {
        this[nameof (AllowShutdown)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("COM3")]
    [DebuggerNonUserCode]
    public string MODEMport
    {
      get
      {
        return (string) this[nameof (MODEMport)];
      }
      set
      {
        this[nameof (MODEMport)] = (object) value;
      }
    }

    [DefaultSettingValue("COM5")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string GPSport
    {
      get
      {
        return (string) this[nameof (GPSport)];
      }
      set
      {
        this[nameof (GPSport)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("5.00")]
    [UserScopedSetting]
    public string MaxV
    {
      get
      {
        return (string) this[nameof (MaxV)];
      }
      set
      {
        this[nameof (MaxV)] = (object) value;
      }
    }

    [DefaultSettingValue("1.00")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string MinV
    {
      get
      {
        return (string) this[nameof (MinV)];
      }
      set
      {
        this[nameof (MinV)] = (object) value;
      }
    }

    [DefaultSettingValue("50.00")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string MaxT
    {
      get
      {
        return (string) this[nameof (MaxT)];
      }
      set
      {
        this[nameof (MaxT)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("-50.00")]
    public string MinT
    {
      get
      {
        return (string) this[nameof (MinT)];
      }
      set
      {
        this[nameof (MinT)] = (object) value;
      }
    }

    [DefaultSettingValue("843.07")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string Pressure
    {
      get
      {
        return (string) this[nameof (Pressure)];
      }
      set
      {
        this[nameof (Pressure)] = (object) value;
      }
    }

    [DefaultSettingValue("5.00")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string AirT
    {
      get
      {
        return (string) this[nameof (AirT)];
      }
      set
      {
        this[nameof (AirT)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("82.31")]
    public string AirS
    {
      get
      {
        return (string) this[nameof (AirS)];
      }
      set
      {
        this[nameof (AirS)] = (object) value;
      }
    }

    [DefaultSettingValue("COM4")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string LCDport
    {
      get
      {
        return (string) this[nameof (LCDport)];
      }
      set
      {
        this[nameof (LCDport)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("1.13")]
    [DebuggerNonUserCode]
    public string vKDAS
    {
      get
      {
        return (string) this[nameof (vKDAS)];
      }
      set
      {
        this[nameof (vKDAS)] = (object) value;
      }
    }

    [DefaultSettingValue("F:")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string USB
    {
      get
      {
        return (string) this[nameof (USB)];
      }
      set
      {
        this[nameof (USB)] = (object) value;
      }
    }

    [DefaultSettingValue("0")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public double raw2VoltCalibration
    {
      get
      {
        return (double) this[nameof (raw2VoltCalibration)];
      }
      set
      {
        this[nameof (raw2VoltCalibration)] = (object) value;
      }
    }
  }
}
