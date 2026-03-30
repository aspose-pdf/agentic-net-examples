using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        int prefValue = editor.GetViewerPreference();

        // Determine page layout
        string pageLayout = "Unknown";
        if ((prefValue & ViewerPreference.PageLayoutOneColumn) != 0)
        {
            pageLayout = "OneColumn";
        }
        else if ((prefValue & ViewerPreference.PageLayoutSinglePage) != 0)
        {
            pageLayout = "SinglePage";
        }
        else if ((prefValue & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
        {
            pageLayout = "TwoColumnLeft";
        }
        else if ((prefValue & ViewerPreference.PageLayoutTwoColumnRight) != 0)
        {
            pageLayout = "TwoColumnRight";
        }

        // Determine zoom/fit setting (FitWindow indicates zoom-to-fit)
        string zoomSetting = "Default";
        if ((prefValue & ViewerPreference.FitWindow) != 0)
        {
            zoomSetting = "FitWindow";
        }

        Console.WriteLine($"Viewer Preference Flags: 0x{prefValue:X}");
        Console.WriteLine($"Page Layout: {pageLayout}");
        Console.WriteLine($"Zoom Setting: {zoomSetting}");

        editor.Close();
    }
}
