using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Page, Color

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "themeConfig.json";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Read user‑selected theme from a simple JSON config file.
        // Expected format: { "theme": "dark" }  // values: "light", "dark", "blue"
        // -----------------------------------------------------------------
        string themeName;
        try
        {
            string json = File.ReadAllText(configPath);
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;
            themeName = root.GetProperty("theme").GetString();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Map theme name to a background color (Aspose.Pdf.Color)
        Aspose.Pdf.Color backgroundColor = themeName?.ToLowerInvariant() switch
        {
            "dark"  => Aspose.Pdf.Color.Black,
            "blue"  => Aspose.Pdf.Color.LightBlue,
            _       => Aspose.Pdf.Color.White // default light theme
        };

        // ---------------------------------------------------------------
        // Load the PDF, apply the background color to every page, save.
        // Document disposal follows the provided lifecycle rule (using).
        // ---------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                page.Background = backgroundColor; // set page background
            }

            // Save the modified PDF (standard Save overload)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with '{themeName}' theme background to '{outputPdfPath}'.");
    }
}