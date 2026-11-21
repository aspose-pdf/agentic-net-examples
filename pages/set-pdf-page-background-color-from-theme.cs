using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string configPath = "theme.config";
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Config file contains a single line with the theme name (e.g., Light, Dark, Blue)
        string theme = File.ReadAllText(configPath).Trim();

        // Determine background color based on theme
        Color bgColor = GetBackgroundColorForTheme(theme);

        // Load PDF, set background for each page, and save
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // The background colour is set on the Page object itself, not on PageInfo
                doc.Pages[i].Background = bgColor;
            }

            doc.Save(outputPdf); // saving as PDF (no SaveOptions needed)
        }

        Console.WriteLine($"PDF saved with '{theme}' theme background to '{outputPdf}'.");
    }

    static Color GetBackgroundColorForTheme(string theme)
    {
        // Map theme names to colors using Aspose.Pdf.Color (RGB values 0‑1)
        switch (theme.ToLowerInvariant())
        {
            case "light":
                return Color.FromRgb(1.0, 1.0, 1.0); // white
            case "dark":
                return Color.FromRgb(0.1, 0.1, 0.1); // dark gray
            case "blue":
                return Color.FromRgb(0.8, 0.9, 1.0); // light blue
            default:
                // Transparent background if theme is unknown
                return Color.Transparent;
        }
    }
}
