// Decompiled with JetBrains decompiler
// Type: iTuner.UsbDisk
// Assembly: KDAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D88A5B4E-9475-405E-AE8F-191C9E1B6D98
// Assembly location: C:\Users\smbrause\AppData\Local\Apps\2.0\4CYGMBHA.OE2\WYOA0OGM.27R\kdas..tion_55564dd1c036797b_0001.0000_6479263921fe5664\KDAS.exe

using System.Text;

namespace iTuner
{
  public class UsbDisk
  {
    private const int KB = 1024;
    private const int MB = 1024000;
    private const int GB = 1024000000;

    internal UsbDisk(string name)
    {
      this.Name = name;
      this.Model = string.Empty;
      this.Volume = string.Empty;
      this.FreeSpace = 0UL;
      this.Size = 0UL;
    }

    public ulong FreeSpace { get; internal set; }

    public string Model { get; internal set; }

    public string Name { get; private set; }

    public ulong Size { get; internal set; }

    public string Volume { get; internal set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.Name);
      stringBuilder.Append(" ");
      stringBuilder.Append(this.Volume);
      stringBuilder.Append(" (");
      stringBuilder.Append(this.Model);
      stringBuilder.Append(") ");
      stringBuilder.Append(this.FormatByteCount(this.FreeSpace));
      stringBuilder.Append(" free of ");
      stringBuilder.Append(this.FormatByteCount(this.Size));
      return stringBuilder.ToString();
    }

    private string FormatByteCount(ulong bytes)
    {
      string str;
      if (bytes < 1024UL)
        str = string.Format("{0} Bytes", (object) bytes);
      else if (bytes < 1024000UL)
      {
        bytes /= 1024UL;
        str = string.Format("{0} KB", (object) bytes.ToString("N"));
      }
      else
        str = bytes >= 1024000000UL ? string.Format("{0} GB", (object) ((double) (bytes / 1024000000UL)).ToString("N1")) : string.Format("{0} MB", (object) ((double) (bytes / 1024000UL)).ToString("N1"));
      return str;
    }
  }
}
