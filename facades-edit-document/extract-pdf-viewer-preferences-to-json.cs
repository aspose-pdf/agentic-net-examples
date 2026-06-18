using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string configPath = "viewerConfig.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve viewer preferences via PdfContentEditor
            using (PdfContentEditor contentEditor = new PdfContentEditor())
            {
                contentEditor.BindPdf(doc);
                int pref = contentEditor.GetViewerPreference();

                // Determine page layout from the preference flags
                string pageLayout = GetPageLayoutFromPreference(pref);

                // Retrieve current zoom coefficient via PdfPageEditor
                using (PdfPageEditor pageEditor = new PdfPageEditor())
                {
                    pageEditor.BindPdf(doc);
                    double zoom = pageEditor.Zoom; // default is 1.0 (100%)

                    // Prepare configuration object
                    ViewerConfig config = new ViewerConfig {
                        PageLayout = pageLayout,
                        Zoom = zoom
                    };

                    // Serialize to JSON and write to file
                    string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(configPath, json);
                    Console.WriteLine($"Viewer configuration saved to '{configPath}'.");
                }
            }
        }
    }

    // Maps ViewerPreference flags to a readable layout name
    static string GetPageLayoutFromPreference(int pref)
    {
        if ((pref & ViewerPreference.PageLayoutOneColumn) != 0) return "OneColumn";
        if ((pref & ViewerPreference.PageLayoutSinglePage) != 0) return "SinglePage";
        if ((pref & ViewerPreference.PageLayoutTwoColumnLeft) != 0) return "TwoColumnLeft";
        if ((pref & ViewerPreference.PageLayoutTwoColumnRight) != 0) return "TwoColumnRight";
        return "Unknown";
    }

    // POCO class for JSON serialization
    class ViewerConfig
    {
        public string PageLayout { get; set; }
        public double Zoom { get; set; }
    }
}