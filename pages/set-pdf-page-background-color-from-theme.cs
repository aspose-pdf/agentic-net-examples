using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string configPath = "themeConfig.json";

        // Verify required files exist
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

        // Load theme configuration (expects JSON like { "Theme": "dark" })
        string json = File.ReadAllText(configPath);
        ThemeConfig config = JsonSerializer.Deserialize<ThemeConfig>(json);

        // Map theme name to an Aspose.Pdf.Color instance
        Aspose.Pdf.Color bgColor = GetColorForTheme(config?.Theme);

        // Load the PDF, set background color for each page, and save
        using (Document doc = new Document(inputPdf)) // document-disposal-with-using rule
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Background = bgColor; // set page background color
            }

            doc.Save(outputPdf); // save the modified PDF
        }

        Console.WriteLine($"PDF saved with theme '{config?.Theme}' to '{outputPdf}'.");
    }

    // Helper to translate theme names into colors
    static Aspose.Pdf.Color GetColorForTheme(string theme)
    {
        if (string.IsNullOrWhiteSpace(theme))
            return Aspose.Pdf.Color.White; // default

        switch (theme.Trim().ToLowerInvariant())
        {
            case "dark":
                return Aspose.Pdf.Color.Black;
            case "light":
                return Aspose.Pdf.Color.White;
            case "sepia":
                return Aspose.Pdf.Color.SaddleBrown;
            case "blue":
                return Aspose.Pdf.Color.LightBlue;
            default:
                return Aspose.Pdf.Color.White;
        }
    }

    // Simple POCO for deserializing the JSON config
    private class ThemeConfig
    {
        public string Theme { get; set; }
    }
}