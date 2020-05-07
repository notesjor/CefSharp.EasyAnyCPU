

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CefSharp.WinForms;

#region

#if LINUX
#else
#endif

#endregion

namespace CefSharp.EasyAnyCPU.WinForms
{
  [ToolboxItem(true)]
  public partial class EasyWebbrowser : UserControl
  {
#if LINUX
    public EasyWebbrowser(){}

    public string MainpageUrl { get; set; }

    public void ExportHtml(){}
    public void ExportImage(){}
    public void ExportPdf(){}
    public void Print(){}
    public void GoToMainpage(){}
#else
    private ChromiumWebBrowser webBrowser1;

    public EasyWebbrowser()
    {
      InitializeComponent();
    }

    public string MainpageUrl { get; set; }
    
    public void ExportImage()
    {
      var sfd = new SaveFileDialog {Filter = "PNG (*.png)|*.png", CheckPathExists = true};
      if (sfd.ShowDialog() == DialogResult.OK)
        ExportImage(sfd.FileName);
    }

    public void ExportImage(string path)
    {
      InitializeBrowser();
      var bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height);
      webBrowser1.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), webBrowser1.Size));
      bitmap.Save(path, ImageFormat.Png);
    }

    public void ExportPdf()
    {
      var sfd = new SaveFileDialog {Filter = "PDF (*.pdf)|*.pdf", CheckPathExists = true};
      if (sfd.ShowDialog() == DialogResult.OK)
        ExportPdf(sfd.FileName);
    }

    public virtual void GoToMainpage()
    {
      InitializeBrowser();

      webBrowser1.Load(MainpageUrl);
    }

    public void LoadHtml(string html)
    {
      InitializeBrowser();
      webBrowser1.LoadHtml(html);
    }

    public void Print()
    {
      InitializeBrowser();
      webBrowser1.Print();
    }

    private void ExportPdf(string path)
    {
      InitializeBrowser();
      webBrowser1.PrintToPdfAsync(path);
    }

    private void InitializeBrowser()
    {
      if (webBrowser1 != null)
        return;

      SuspendLayout();
      webBrowser1 = StaticBrowserHandler.Get(Size);
      Controls.Add(webBrowser1);
      ResumeLayout(false);
    }
#endif
  }
}