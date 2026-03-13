// Decompiled with JetBrains decompiler
// Type: iTuner.UsbDiskCollection
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace iTuner
{
  public class UsbDiskCollection : ObservableCollection<UsbDisk>
  {
    public bool Contains(string name)
    {
      return this.AsQueryable<UsbDisk>().Any<UsbDisk>((Expression<Func<UsbDisk, bool>>) (d => d.Name == name));
    }

    public bool Remove(string name)
    {
      UsbDisk usbDisk = this.AsQueryable<UsbDisk>().Where<UsbDisk>((Expression<Func<UsbDisk, bool>>) (d => d.Name == name)).Select<UsbDisk, UsbDisk>((Expression<Func<UsbDisk, UsbDisk>>) (d => d)).FirstOrDefault<UsbDisk>();
      if (usbDisk != null)
        return this.Remove(usbDisk);
      return false;
    }
  }
}
