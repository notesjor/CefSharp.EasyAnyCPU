﻿using System;
using System.IO;
using System.Net;
using System.Reflection;
using Telerik.WinControls.Zip.Extensions;

namespace CefSharp.EasyAnyCPU.Installer
{
  public static class BrowserInstaller
  {
    public static bool IsInstalled()
      => File.Exists(GetBrowserPath());

    public static void EnsureInstallation()
    {
      if (!IsInstalled())
        Install();
    }

    public static void Install()
      => Install(GetBrowserPath());

    public static void Install(string path)
    {
      var url = $"http://www.bitcutstudios.com/products/corpusexplorer/WebBrowser_{ (Environment.Is64BitProcess ? "x64" : "x86") }.zip";
      var tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".zip");
      using (var wc = new WebClient())
        wc.DownloadFile(url, tmp);
      var dir = Path.GetDirectoryName(path);

      ZipFile.ExtractToDirectory(tmp, dir);
    }

    private static string GetBrowserPath() 
      => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CefSharp.BrowserSubprocess.exe");
  }
}
