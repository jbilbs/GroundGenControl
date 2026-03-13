// Decompiled with JetBrains decompiler
// Type: iTuner.UsbManager
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace iTuner
{
  public class UsbManager : IDisposable
  {
    private UsbManager.DriverWindow window;
    private UsbStateChangedEventHandler handler;
    private bool isDisposed;

    public UsbManager()
    {
      this.window = (UsbManager.DriverWindow) null;
      this.handler = (UsbStateChangedEventHandler) null;
      this.isDisposed = false;
    }

    ~UsbManager()
    {
      this.Dispose();
    }

    public void Dispose()
    {
      if (this.isDisposed)
        return;
      if (this.window != null)
      {
        this.window.StateChanged -= new UsbStateChangedEventHandler(this.DoStateChanged);
        this.window.Dispose();
        this.window = (UsbManager.DriverWindow) null;
      }
      this.isDisposed = true;
      GC.SuppressFinalize((object) this);
    }

    public event UsbStateChangedEventHandler StateChanged
    {
      add
      {
        if (this.window == null)
        {
          this.window = new UsbManager.DriverWindow();
          this.window.StateChanged += new UsbStateChangedEventHandler(this.DoStateChanged);
        }
        this.handler += value;
      }
      remove
      {
        this.handler -= value;
        if (this.handler != null)
          return;
        this.window.StateChanged -= new UsbStateChangedEventHandler(this.DoStateChanged);
        this.window.Dispose();
        this.window = (UsbManager.DriverWindow) null;
      }
    }

    public UsbDiskCollection GetAvailableDisks()
    {
      UsbDiskCollection usbDiskCollection = new UsbDiskCollection();
      foreach (ManagementObject managementObject1 in new ManagementObjectSearcher("select DeviceID, Model from Win32_DiskDrive where InterfaceType='USB'").Get())
      {
        ManagementObject managementObject2 = new ManagementObjectSearcher(string.Format("associators of {{Win32_DiskDrive.DeviceID='{0}'}} where AssocClass = Win32_DiskDriveToDiskPartition", managementObject1["DeviceID"])).First();
        if (managementObject2 != null)
        {
          ManagementObject managementObject3 = new ManagementObjectSearcher(string.Format("associators of {{Win32_DiskPartition.DeviceID='{0}'}} where AssocClass = Win32_LogicalDiskToPartition", managementObject2["DeviceID"])).First();
          if (managementObject3 != null)
          {
            ManagementObject managementObject4 = new ManagementObjectSearcher(string.Format("select FreeSpace, Size, VolumeName from Win32_LogicalDisk where Name='{0}'", managementObject3["Name"])).First();
            usbDiskCollection.Add(new UsbDisk(managementObject3["Name"].ToString())
            {
              Model = managementObject1["Model"].ToString(),
              Volume = managementObject4["VolumeName"].ToString(),
              FreeSpace = (ulong) managementObject4["FreeSpace"],
              Size = (ulong) managementObject4["Size"]
            });
          }
        }
      }
      return usbDiskCollection;
    }

    private void DoStateChanged(UsbStateChangedEventArgs e)
    {
      if (this.handler == null)
        return;
      UsbDisk disk = e.Disk;
      if (e.State == UsbStateChange.Added && (int) e.Disk.Name[0] != 63)
      {
        UsbManager.GetDiskInformationDelegate informationDelegate = new UsbManager.GetDiskInformationDelegate(this.GetDiskInformation);
        IAsyncResult result = informationDelegate.BeginInvoke(e.Disk, (AsyncCallback) null, (object) null);
        informationDelegate.EndInvoke(result);
      }
      this.handler(e);
    }

    private void GetDiskInformation(UsbDisk disk)
    {
      ManagementObject managementObject1 = new ManagementObjectSearcher(string.Format("associators of {{Win32_LogicalDisk.DeviceID='{0}'}} where AssocClass = Win32_LogicalDiskToPartition", (object) disk.Name)).First();
      if (managementObject1 == null)
        return;
      ManagementObject managementObject2 = new ManagementObjectSearcher(string.Format("associators of {{Win32_DiskPartition.DeviceID='{0}'}}  where resultClass = Win32_DiskDrive", managementObject1["DeviceID"])).First();
      if (managementObject2 != null)
        disk.Model = managementObject2["Model"].ToString();
      ManagementObject managementObject3 = new ManagementObjectSearcher(string.Format("select FreeSpace, Size, VolumeName from Win32_LogicalDisk where Name='{0}'", (object) disk.Name)).First();
      if (managementObject3 == null)
        return;
      disk.Volume = managementObject3["VolumeName"].ToString();
      disk.FreeSpace = (ulong) managementObject3["FreeSpace"];
      disk.Size = (ulong) managementObject3["Size"];
    }

    private class DriverWindow : NativeWindow, IDisposable
    {
      private const int WM_DEVICECHANGE = 537;
      private const int DBT_DEVICEARRIVAL = 32768;
      private const int DBT_DEVICEQUERYREMOVE = 32769;
      private const int DBT_DEVICEREMOVECOMPLETE = 32772;
      private const int DBT_DEVTYP_VOLUME = 2;

      public DriverWindow()
      {
        this.CreateHandle(new CreateParams());
      }

      public void Dispose()
      {
        this.DestroyHandle();
        GC.SuppressFinalize((object) this);
      }

      public event UsbStateChangedEventHandler StateChanged;

      protected override void WndProc(ref Message message)
      {
        base.WndProc(ref message);
        if (message.Msg != 537 || !(message.LParam != IntPtr.Zero))
          return;
        UsbManager.DriverWindow.DEV_BROADCAST_VOLUME structure = (UsbManager.DriverWindow.DEV_BROADCAST_VOLUME) Marshal.PtrToStructure(message.LParam, typeof (UsbManager.DriverWindow.DEV_BROADCAST_VOLUME));
        if (structure.dbcv_devicetype != 2)
          return;
        switch (message.WParam.ToInt32())
        {
          case 32768:
            this.SignalDeviceChange(UsbStateChange.Added, structure);
            break;
          case 32772:
            this.SignalDeviceChange(UsbStateChange.Removed, structure);
            break;
        }
      }

      private void SignalDeviceChange(UsbStateChange state, UsbManager.DriverWindow.DEV_BROADCAST_VOLUME volume)
      {
        string unitName = this.ToUnitName(volume.dbcv_unitmask);
        if (this.StateChanged == null)
          return;
        UsbDisk disk = new UsbDisk(unitName);
        this.StateChanged(new UsbStateChangedEventArgs(state, disk));
      }

      private string ToUnitName(int mask)
      {
        int num;
        for (num = 0; num < 26 && (mask & 1) == 0; ++num)
          mask >>= 1;
        if (num < 26)
          return string.Format("{0}:", (object) Convert.ToChar(Convert.ToInt32('A') + num));
        return "?:";
      }

      public struct DEV_BROADCAST_VOLUME
      {
        public int dbcv_size;
        public int dbcv_devicetype;
        public int dbcv_reserved;
        public int dbcv_unitmask;
        public short dbcv_flags;
      }
    }

    private delegate void GetDiskInformationDelegate(UsbDisk disk);
  }
}
