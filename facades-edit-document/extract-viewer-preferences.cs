using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace ViewerPreferencesExample
{
    public class ViewerConfig
    {
        public string PageLayout { get; set; }
        public double Zoom { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            string inputPdfPath = "input.pdf";
            string outputJsonPath = "viewer-config.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"File not found: {inputPdfPath}");
                return;
            }

            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);
            int preferenceFlags = editor.GetViewerPreference();

            // Determine page layout from flags
            string pageLayout = "Unknown";
            if ((preferenceFlags & ViewerPreference.PageLayoutOneColumn) != 0)
            {
                pageLayout = "OneColumn";
            }
            else if ((preferenceFlags & ViewerPreference.PageLayoutSinglePage) != 0)
            {
                pageLayout = "SinglePage";
            }
            else if ((preferenceFlags & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
            {
                pageLayout = "TwoColumnLeft";
            }
            else if ((preferenceFlags & ViewerPreference.PageLayoutTwoColumnRight) != 0)
            {
                pageLayout = "TwoColumnRight";
            }

            // Zoom factor is not directly exposed via viewer preferences; use default value
            double zoomFactor = 1.0;

            ViewerConfig config = new ViewerConfig();
            config.PageLayout = pageLayout;
            config.Zoom = zoomFactor;

            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Viewer configuration saved to '{outputJsonPath}'.");
        }
    }
}