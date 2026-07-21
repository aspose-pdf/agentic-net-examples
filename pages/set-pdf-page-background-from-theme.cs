using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string configPath = "themeConfig.json";

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Load theme configuration (expects JSON like { "Theme": "Dark" })
        string json = File.ReadAllText(configPath);
        ThemeConfig cfg = JsonSerializer.Deserialize<ThemeConfig>(json);

        // Determine background color based on the selected theme
        Aspose.Pdf.Color bgColor = GetColorForTheme(cfg?.Theme);

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Apply the background color to every page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = doc.Pages[i];
                page.Background = bgColor;
            }

            // Save the modified PDF (standard Save overload)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}' with background color {bgColor}.");
    }

    // Maps a theme name to an Aspose.Pdf.Color instance
    static Aspose.Pdf.Color GetColorForTheme(string theme)
    {
        return theme?.Trim().ToLowerInvariant() switch
        {
            "dark"   => Aspose.Pdf.Color.Black,
            "light"  => Aspose.Pdf.Color.White,
            "blue"   => Aspose.Pdf.Color.LightBlue,
            "sepia"  => Aspose.Pdf.Color.LightGray,
            _        => Aspose.Pdf.Color.White // default fallback
        };
    }

    // Simple POCO for deserializing the JSON configuration
    private class ThemeConfig
    {
        public string Theme { get; set; }
    }
}