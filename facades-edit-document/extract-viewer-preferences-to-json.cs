using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "viewerConfig.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Retrieve viewer preference flags using PdfContentEditor
        int prefValue;
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            prefValue = editor.GetViewerPreference();
        }

        // Determine page layout from ViewerPreference flags
        string pageLayout = GetPageLayout(prefValue);

        // Retrieve current zoom (scale factor) using PdfViewer
        float zoomFactor;
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(inputPdf);
            zoomFactor = viewer.ScaleFactor; // default is 1.0 (100%)
        }

        // Prepare configuration object
        var config = new
        {
            Layout = pageLayout,
            Zoom = zoomFactor
        };

        // Serialize to JSON with indentation
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

        // Save JSON configuration file
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Viewer configuration saved to '{outputJson}'.");
    }

    // Helper method to map ViewerPreference flags to a readable layout string
    private static string GetPageLayout(int prefValue)
    {
        // ViewerPreference flags are defined in Aspose.Pdf.Facades.ViewerPreference
        if ((prefValue & ViewerPreference.PageLayoutOneColumn) != 0)
            return "OneColumn";
        if ((prefValue & ViewerPreference.PageLayoutSinglePage) != 0)
            return "SinglePage";
        if ((prefValue & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
            return "TwoColumnLeft";
        if ((prefValue & ViewerPreference.PageLayoutTwoColumnRight) != 0)
            return "TwoColumnRight";

        // Default fallback if none of the layout flags are set
        return "Unknown";
    }
}