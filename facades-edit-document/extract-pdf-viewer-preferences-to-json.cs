using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";
        const string jsonPath = "viewerConfig.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // ----- Retrieve viewer preference flags -----
        int prefValue;
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(pdfPath);
            prefValue = contentEditor.GetViewerPreference();
        }

        // ----- Decode page layout from flags -----
        string pageLayout = DecodePageLayout(prefValue);

        // ----- Retrieve current zoom factor (default is 1.0) -----
        double zoomFactor;
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(pdfPath);
            zoomFactor = pageEditor.Zoom; // get current zoom coefficient
        }

        // ----- Build configuration object and serialize to JSON -----
        var config = new
        {
            PageLayout = pageLayout,
            Zoom       = zoomFactor
        };

        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Viewer configuration saved to '{jsonPath}'.");
    }

    // Helper to map ViewerPreference flags to a readable layout name
    static string DecodePageLayout(int pref)
    {
        if ((pref & ViewerPreference.PageLayoutOneColumn)      != 0) return "OneColumn";
        if ((pref & ViewerPreference.PageLayoutSinglePage)    != 0) return "SinglePage";
        if ((pref & ViewerPreference.PageLayoutTwoColumnLeft) != 0) return "TwoColumnLeft";
        if ((pref & ViewerPreference.PageLayoutTwoColumnRight)!= 0) return "TwoColumnRight";
        return "Unknown";
    }
}