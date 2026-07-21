using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class ViewerPreferenceConfig
{
    public int ViewerPreferenceValue { get; set; }
    public string PageLayout { get; set; }
    public string ZoomMode { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "viewer_preferences.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use PdfContentEditor facade to read viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            int prefValue = editor.GetViewerPreference();

            // Determine page layout flag
            string layout;
            if ((prefValue & ViewerPreference.PageLayoutOneColumn) != 0)
                layout = "OneColumn";
            else if ((prefValue & ViewerPreference.PageLayoutSinglePage) != 0)
                layout = "SinglePage";
            else if ((prefValue & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
                layout = "TwoColumnLeft";
            else if ((prefValue & ViewerPreference.PageLayoutTwoColumnRight) != 0)
                layout = "TwoColumnRight";
            else
                layout = "Unknown";

            // Determine a simple zoom mode flag (FitWindow indicates a fit‑to‑window zoom)
            string zoomMode = (prefValue & ViewerPreference.FitWindow) != 0 ? "FitWindow" : "Default";

            // Prepare configuration object
            ViewerPreferenceConfig config = new ViewerPreferenceConfig {
                ViewerPreferenceValue = prefValue,
                PageLayout = layout,
                ZoomMode = zoomMode
            };

            // Serialize to JSON with indentation for readability
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Viewer preferences saved to '{outputJsonPath}'.");
        }
    }
}