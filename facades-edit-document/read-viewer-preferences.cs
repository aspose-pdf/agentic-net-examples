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

        // Determine page layout flag
        string layout = "Unknown";
        if ((prefValue & ViewerPreference.PageLayoutOneColumn) != 0)
        {
            layout = "OneColumn";
        }
        else if ((prefValue & ViewerPreference.PageLayoutSinglePage) != 0)
        {
            layout = "SinglePage";
        }
        else if ((prefValue & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
        {
            layout = "TwoColumnLeft";
        }
        else if ((prefValue & ViewerPreference.PageLayoutTwoColumnRight) != 0)
        {
            layout = "TwoColumnRight";
        }

        // Zoom settings are not directly exposed via ViewerPreference; log raw value
        Console.WriteLine($"Viewer Preference value: {prefValue}");
        Console.WriteLine($"Page Layout: {layout}");
    }
}