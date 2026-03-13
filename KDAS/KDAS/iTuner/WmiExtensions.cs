// Decompiled with JetBrains decompiler
// Type: iTuner.WmiExtensions
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using System.Management;

namespace iTuner
{
  public static class WmiExtensions
  {
    public static ManagementObject First(this ManagementObjectSearcher searcher)
    {
      ManagementObject managementObject = (ManagementObject) null;
      using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = searcher.Get().GetEnumerator())
      {
        if (enumerator.MoveNext())
          managementObject = (ManagementObject) enumerator.Current;
      }
      return managementObject;
    }
  }
}
