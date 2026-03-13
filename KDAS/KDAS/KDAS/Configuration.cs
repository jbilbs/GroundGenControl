// Decompiled with JetBrains decompiler
// Type: KDAS.Configuration
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using iTuner;
using KDAS.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace KDAS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public partial class Configuration : Window, IComponentConnector
  {
    private static readonly string CR = Environment.NewLine;
    private string[] lPorts;
    private ObservableCollection<string> CommPorts;
    private UsbManager manager;
    private UsbDiskCollection disks;

    public event EventHandler<updateTfReceivedArg> updateTfReceived;

    public Configuration()
    {
      this.InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      double raw2VoltCalibration = Settings.Default.raw2VoltCalibration;
      this.sldManualCal.Value = raw2VoltCalibration;
      this.txbManualCal.Text = Convert.ToString(raw2VoltCalibration);
      this.CommPorts = new ObservableCollection<string>();
      this.lPorts = SerialPort.GetPortNames();
      this.manager = new UsbManager();
      this.disks = this.manager.GetAvailableDisks();
      for (int index = 0; index < this.lPorts.Length; ++index)
        this.CommPorts.Add(this.lPorts[index]);
      this.lbModem.ItemsSource = (IEnumerable) this.CommPorts;
      this.lbGPS.ItemsSource = (IEnumerable) this.CommPorts;
      this.lbLCD.ItemsSource = (IEnumerable) this.CommPorts;
      string gpSport = Settings.Default.GPSport;
      string modeMport = Settings.Default.MODEMport;
      string lcDport = Settings.Default.LCDport;
      for (int index = 0; index < this.CommPorts.Count; ++index)
      {
        if (this.lPorts[index] == gpSport)
        {
          this.lbGPS.SelectedIndex = index;
          this.txbGPScurrent.Text = gpSport;
        }
        if (this.lPorts[index] == modeMport)
        {
          this.lbModem.SelectedIndex = index;
          this.txbMODEMcurrent.Text = modeMport;
        }
        if (this.lPorts[index] == lcDport)
        {
          this.lbLCD.SelectedIndex = index;
          this.txbLCDcurrent.Text = lcDport;
        }
      }
      this.txbUSB.AppendText(Configuration.CR);
      this.txbUSB.AppendText("Available USB disks" + Configuration.CR);
      foreach (UsbDisk disk in (Collection<UsbDisk>) this.disks)
      {
        this.txbUSB.AppendText(disk.ToString() + Configuration.CR);
        this.lbUSB.Items.Add((object) disk.Name);
      }
      this.txbUSB.AppendText(Configuration.CR);
      this.manager.StateChanged += new UsbStateChangedEventHandler(this.DoStateChanged);
      this.cbAllowShutdown.IsChecked = new bool?(Settings.Default.AllowShutdown);
      this.txbMaxTemp.Text = Settings.Default.MaxT;
      this.txbMinTemp.Text = Settings.Default.MinT;
      this.txbMaxVolt.Text = Settings.Default.MaxV;
      this.txbMinVolt.Text = Settings.Default.MinV;
      this.txbLWpressure.Text = Settings.Default.Pressure;
      this.txbLWairTemp.Text = Settings.Default.AirT;
      this.txbLWairSpeed.Text = Settings.Default.AirS;
      this.txbCurTail.Text = Settings.Default.TailNumber;
      this.txbDelay.Text = Settings.Default.TXtime;
      this.btnTempUpdate((object) null, (RoutedEventArgs) null);
      this.btnLWupdate((object) null, (RoutedEventArgs) null);
      this.txbCurrentUSB.Text = Settings.Default.USB;
    }

    private void DoStateChanged(UsbStateChangedEventArgs e)
    {
      this.txbUSB.AppendText(((int) e.State).ToString() + " " + e.Disk.ToString() + Configuration.CR);
      if (e.State.Equals((object) UsbStateChange.Removed))
      {
        this.lbUSB.Items.Remove((object) e.Disk.Name);
      }
      else
      {
        if (!e.State.Equals((object) UsbStateChange.Added))
          return;
        this.lbUSB.Items.Add((object) e.Disk.Name);
      }
    }

    private void btnSetGPS_Click(object sender, RoutedEventArgs e)
    {
      Settings.Default.GPSport = this.lPorts[this.lbGPS.SelectedIndex];
      Settings.Default.Save();
      this.txbGPScurrent.Text = this.lPorts[this.lbGPS.SelectedIndex];
    }

    private void btnSetModem_Click(object sender, RoutedEventArgs e)
    {
      Settings.Default.MODEMport = this.lPorts[this.lbModem.SelectedIndex];
      Settings.Default.Save();
      this.txbMODEMcurrent.Text = this.lPorts[this.lbModem.SelectedIndex];
    }

    private void cbAllowShutdown_Unchecked(object sender, RoutedEventArgs e)
    {
      Settings.Default.AllowShutdown = false;
      Settings.Default.Save();
    }

    private void cbAllowShutdown_Checked(object sender, RoutedEventArgs e)
    {
      Settings.Default.AllowShutdown = true;
      Settings.Default.Save();
    }

    private void btnTempDefault(object sender, RoutedEventArgs e)
    {
      Settings.Default.MaxT = "50.00";
      Settings.Default.MinT = "-50.00";
      Settings.Default.MaxV = "5.00";
      Settings.Default.MinV = "1.00";
      this.txbMaxTemp.Text = "50.00";
      this.txbMinTemp.Text = "-50.00";
      this.txbMaxVolt.Text = "5.00";
      this.txbMinVolt.Text = "1.00";
      Settings.Default.Save();
      this.btnTempUpdate((object) null, (RoutedEventArgs) null);
    }

    private void btnTempUpdate(object sender, RoutedEventArgs e)
    {
      try
      {
        double num1 = Convert.ToDouble(this.txbMaxTemp.Text);
        double num2 = Convert.ToDouble(this.txbMinTemp.Text);
        double num3 = Convert.ToDouble(this.txbMaxVolt.Text);
        double num4 = Convert.ToDouble(this.txbMinVolt.Text);
        char ch = ' ';
        double num5 = (num1 - num2) / (num3 - num4);
        double num6 = num1 - num5 * num3;
        string str1 = string.Format("{0:N2}", (object) num5);
        string str2 = string.Format("{0:N2}", (object) num6);
        string str3 = string.Format("{0:N2}", (object) num1);
        string str4 = string.Format("{0:N2}", (object) num2);
        string str5 = string.Format("{0:N2}", (object) num3);
        string str6 = string.Format("{0:N2}", (object) num4);
        if (num6 > 0.0)
          ch = '+';
        if (num6 == 0.0)
          str2 = "";
        this.txbTempTrans.Text = "T(v) = " + str1 + "v" + (object) ch + str2;
        this.txbMaxTemp.Text = str3;
        this.txbMinTemp.Text = str4;
        this.txbMaxVolt.Text = str5;
        this.txbMinVolt.Text = str6;
        Settings.Default.MaxT = str3;
        Settings.Default.MinT = str4;
        Settings.Default.MaxV = str5;
        Settings.Default.MinV = str6;
        Settings.Default.Save();
      }
      catch
      {
        int num = (int) MessageBox.Show("Recheck Your Parameters!");
      }
    }

    private void btnLWdefault(object sender, RoutedEventArgs e)
    {
      Settings.Default.Pressure = "843.07";
      Settings.Default.AirT = "5.00";
      Settings.Default.AirS = "82.31";
      Settings.Default.Save();
      this.txbLWpressure.Text = "843.07";
      this.txbLWairTemp.Text = "5.00";
      this.txbLWairSpeed.Text = "82.31";
      this.btnLWupdate((object) null, (RoutedEventArgs) null);
    }

    private void btnLWupdate(object sender, RoutedEventArgs e)
    {
      try
      {
        double num1 = Convert.ToDouble(this.txbLWpressure.Text);
        double num2 = Convert.ToDouble(this.txbLWairTemp.Text);
        double num3 = Convert.ToDouble(this.txbLWairSpeed.Text);
        string str1 = string.Format("{0:N2}", (object) num1);
        string str2 = string.Format("{0:N2}", (object) num2);
        string str3 = string.Format("{0:N2}", (object) num3);
        this.txbLWpressure.Text = str1;
        this.txbLWairTemp.Text = str2;
        this.txbLWairSpeed.Text = str3;
        Settings.Default.Pressure = str1;
        Settings.Default.AirT = str2;
        Settings.Default.AirS = str3;
        Settings.Default.Save();
      }
      catch
      {
        int num = (int) MessageBox.Show("Recheck Your Parameters!");
      }
    }

    private void btnTailUpdate_Click(object sender, RoutedEventArgs e)
    {
      if (this.txbNewTail.Text.Length >= 4)
      {
        Settings.Default.TailNumber = this.txbNewTail.Text;
        Settings.Default.Save();
        this.txbCurTail.Text = Settings.Default.TailNumber;
        this.txbNewTail.Text = "";
      }
      else
      {
        int num = (int) MessageBox.Show("Tail Number must be at least 4 characters");
      }
    }

    private void btnDelayUpdate_Click(object sender, RoutedEventArgs e)
    {
      if (!(this.txbDelay.Text == "1") && !(this.txbDelay.Text == "2") && (!(this.txbDelay.Text == "3") && !(this.txbDelay.Text == "4")) && !(this.txbDelay.Text == "5"))
        return;
      Settings.Default.TXtime = this.txbDelay.Text;
      Settings.Default.Save();
      this.txbDelay.Text = Settings.Default.TXtime;
    }

    private void btnSetLCD_Click(object sender, RoutedEventArgs e)
    {
      Settings.Default.LCDport = this.lPorts[this.lbLCD.SelectedIndex];
      Settings.Default.Save();
      this.txbLCDcurrent.Text = this.lPorts[this.lbLCD.SelectedIndex];
    }

    private void btnUSBupdate_Click(object sender, RoutedEventArgs e)
    {
      Settings.Default.USB = this.disks[this.lbUSB.SelectedIndex].Name;
      Settings.Default.Save();
      this.txbCurrentUSB.Text = this.disks[this.lbUSB.SelectedIndex].Name;
      int num = (int) MessageBox.Show("You must restart KDAS");
    }

    private void sldManualCal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      this.txbManualCal.Text = this.sldManualCal.Value.ToString("0.00");
    }

    private void btnManualCal_Click(object sender, RoutedEventArgs e)
    {
      Settings.Default.raw2VoltCalibration = this.sldManualCal.Value;
      updateTfReceivedArg e1 = new updateTfReceivedArg();
      if (this.updateTfReceived == null)
        return;
      this.updateTfReceived((object) this, e1);
    }
    
    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.Window_Loaded);
          break;
        case 2:
          this.tabControl1 = (TabControl) target;
          break;
        case 3:
          this.groupBox6 = (GroupBox) target;
          break;
        case 4:
          this.txbDelay = (TextBox) target;
          break;
        case 5:
          this.label23 = (Label) target;
          break;
        case 6:
          this.label24 = (Label) target;
          break;
        case 7:
          this.btnDelayUpdate = (Button) target;
          this.btnDelayUpdate.Click += new RoutedEventHandler(this.btnDelayUpdate_Click);
          break;
        case 8:
          this.groupBox5 = (GroupBox) target;
          break;
        case 9:
          this.txbCurTail = (TextBox) target;
          break;
        case 10:
          this.label21 = (Label) target;
          break;
        case 11:
          this.btnTailUpdate = (Button) target;
          this.btnTailUpdate.Click += new RoutedEventHandler(this.btnTailUpdate_Click);
          break;
        case 12:
          this.txbNewTail = (TextBox) target;
          break;
        case 13:
          this.label22 = (Label) target;
          break;
        case 14:
          this.groupBox3 = (GroupBox) target;
          break;
        case 15:
          this.cbAllowShutdown = (CheckBox) target;
          this.cbAllowShutdown.Unchecked += new RoutedEventHandler(this.cbAllowShutdown_Unchecked);
          this.cbAllowShutdown.Checked += new RoutedEventHandler(this.cbAllowShutdown_Checked);
          break;
        case 16:
          this.tabItem1 = (TabItem) target;
          break;
        case 17:
          this.groupBox2 = (GroupBox) target;
          break;
        case 18:
          this.button1 = (Button) target;
          this.button1.Click += new RoutedEventHandler(this.btnTempDefault);
          break;
        case 19:
          this.button2 = (Button) target;
          this.button2.Click += new RoutedEventHandler(this.btnTempUpdate);
          break;
        case 20:
          this.txbMaxVolt = (TextBox) target;
          break;
        case 21:
          this.label7 = (Label) target;
          break;
        case 22:
          this.txbMinVolt = (TextBox) target;
          break;
        case 23:
          this.label6 = (Label) target;
          break;
        case 24:
          this.txbMaxTemp = (TextBox) target;
          break;
        case 25:
          this.label8 = (Label) target;
          break;
        case 26:
          this.txbMinTemp = (TextBox) target;
          break;
        case 27:
          this.label9 = (Label) target;
          break;
        case 28:
          this.txbTempTrans = (TextBox) target;
          break;
        case 29:
          this.label10 = (Label) target;
          break;
        case 30:
          this.label13 = (Label) target;
          break;
        case 31:
          this.label15 = (Label) target;
          break;
        case 32:
          this.label16 = (Label) target;
          break;
        case 33:
          this.label17 = (Label) target;
          break;
        case 34:
          this.groupBox4 = (GroupBox) target;
          break;
        case 35:
          this.button3 = (Button) target;
          this.button3.Click += new RoutedEventHandler(this.btnLWdefault);
          break;
        case 36:
          this.button4 = (Button) target;
          this.button4.Click += new RoutedEventHandler(this.btnLWupdate);
          break;
        case 37:
          this.txbLWpressure = (TextBox) target;
          break;
        case 38:
          this.label5 = (Label) target;
          break;
        case 39:
          this.txbLWairTemp = (TextBox) target;
          break;
        case 40:
          this.label11 = (Label) target;
          break;
        case 41:
          this.txbLWairSpeed = (TextBox) target;
          break;
        case 42:
          this.label12 = (Label) target;
          break;
        case 43:
          this.label14 = (Label) target;
          break;
        case 44:
          this.textBox1 = (TextBox) target;
          break;
        case 45:
          this.label18 = (Label) target;
          break;
        case 46:
          this.label19 = (Label) target;
          break;
        case 47:
          this.label20 = (Label) target;
          break;
        case 48:
          this.label26 = (Label) target;
          break;
        case 49:
          this.groupBox8 = (GroupBox) target;
          break;
        case 50:
          this.btnManualCal = (Button) target;
          this.btnManualCal.Click += new RoutedEventHandler(this.btnManualCal_Click);
          break;
        case 51:
          this.sldManualCal = (Slider) target;
          this.sldManualCal.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sldManualCal_ValueChanged);
          break;
        case 52:
          this.txbManualCal = (TextBox) target;
          break;
        case 53:
          this.groupBox1 = (GroupBox) target;
          break;
        case 54:
          this.lbGPS = (ListBox) target;
          break;
        case 55:
          this.lbModem = (ListBox) target;
          break;
        case 56:
          this.label1 = (Label) target;
          break;
        case 57:
          this.label2 = (Label) target;
          break;
        case 58:
          this.btnSetGPS = (Button) target;
          this.btnSetGPS.Click += new RoutedEventHandler(this.btnSetGPS_Click);
          break;
        case 59:
          this.btnSetModem = (Button) target;
          this.btnSetModem.Click += new RoutedEventHandler(this.btnSetModem_Click);
          break;
        case 60:
          this.txbGPScurrent = (TextBox) target;
          break;
        case 61:
          this.txbMODEMcurrent = (TextBox) target;
          break;
        case 62:
          this.label3 = (Label) target;
          break;
        case 63:
          this.label4 = (Label) target;
          break;
        case 64:
          this.lbLCD = (ListBox) target;
          break;
        case 65:
          this.label25 = (Label) target;
          break;
        case 66:
          this.btnSetLCD = (Button) target;
          this.btnSetLCD.Click += new RoutedEventHandler(this.btnSetLCD_Click);
          break;
        case 67:
          this.txbLCDcurrent = (TextBox) target;
          break;
        case 68:
          this.groupBox7 = (GroupBox) target;
          break;
        case 69:
          this.txbUSB = (TextBox) target;
          break;
        case 70:
          this.btnUSBupdate = (Button) target;
          this.btnUSBupdate.Click += new RoutedEventHandler(this.btnUSBupdate_Click);
          break;
        case 71:
          this.lbUSB = (ListBox) target;
          break;
        case 72:
          this.txbCurrentUSB = (TextBox) target;
          break;
        case 73:
          this.label27 = (Label) target;
          break;
        case 74:
          this.label28 = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
