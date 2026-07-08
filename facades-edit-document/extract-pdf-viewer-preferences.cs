using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class ViewerPreferenceConfig
{
    public string Layout { get; set; }
    public string Zoom { get; set; }
}

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

        // Use PdfContentEditor (a Facade) to read viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            int prefValue = editor.GetViewerPreference();

            // Determine page layout from ViewerPreference flags
            string layout = prefValue switch
            {
                var v when (v & ViewerPreference.PageLayoutOneColumn) != 0 => "OneColumn",
                var v when (v & ViewerPreference.PageLayoutSinglePage) != 0 => "SinglePage",
                var v when (v & ViewerPreference.PageLayoutTwoColumnLeft) != 0 => "TwoColumnLeft",
                var v when (v & ViewerPreference.PageLayoutTwoColumnRight) != 0 => "TwoColumnRight",
                _ => "Unknown"
            };

            // Determine zoom setting; FitWindow flag indicates a fit‑to‑window zoom
            string zoom = (prefValue & ViewerPreference.FitWindow) != 0 ? "FitWindow" : "100%";

            // Build configuration object
            ViewerPreferenceConfig config = new ViewerPreferenceConfig {
                Layout = layout,
                Zoom = zoom
            };

            // Serialize to JSON and write to file
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Viewer preferences saved to '{outputJson}'.");
    }
}