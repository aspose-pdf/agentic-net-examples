using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string configJson = "viewerConfig.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // ----- Retrieve viewer preferences -----
        int prefValue;
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(inputPdf);
            prefValue = contentEditor.GetViewerPreference();
        }

        // Determine page layout flag from the preference bits
        string pageLayout = GetPageLayoutFromPreference(prefValue);

        // ----- Retrieve zoom coefficient -----
        double zoom = 1.0; // default if not set
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPdf);
            zoom = pageEditor.Zoom; // gets the current zoom factor
        }

        // ----- Store results in JSON -----
        ViewerConfig config = new ViewerConfig {
            PageLayout = pageLayout,
            Zoom = zoom
        };

        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(configJson, json);
        Console.WriteLine($"Configuration saved to '{configJson}'.");
    }

    // Helper to translate preference bits into a readable layout name
    static string GetPageLayoutFromPreference(int pref)
    {
        if ((pref & (int)ViewerPreference.PageLayoutOneColumn) != 0)      return "OneColumn";
        if ((pref & (int)ViewerPreference.PageLayoutSinglePage) != 0)    return "SinglePage";
        if ((pref & (int)ViewerPreference.PageLayoutTwoColumnLeft) != 0) return "TwoColumnLeft";
        if ((pref & (int)ViewerPreference.PageLayoutTwoColumnRight) != 0) return "TwoColumnRight";
        return "Unknown";
    }

    // Simple DTO for JSON serialization
    private class ViewerConfig
    {
        public string PageLayout { get; set; }
        public double Zoom { get; set; }
    }
}