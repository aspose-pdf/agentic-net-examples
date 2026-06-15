using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Simple configuration model
    private class ThemeConfig
    {
        public string Theme { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "themeConfig.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Load user‑selected theme from JSON config
        ThemeConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<ThemeConfig>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // Determine background color based on theme
        Aspose.Pdf.Color bgColor = Aspose.Pdf.Color.White; // default
        if (config?.Theme != null)
        {
            switch (config.Theme.Trim().ToLowerInvariant())
            {
                case "dark":
                    bgColor = Aspose.Pdf.Color.Black;
                    break;
                case "light":
                    bgColor = Aspose.Pdf.Color.White;
                    break;
                case "sepia":
                    bgColor = Aspose.Pdf.Color.PapayaWhip;
                    break;
                // add more themes as needed
                default:
                    Console.WriteLine($"Unknown theme '{config.Theme}'. Using default (White).");
                    break;
            }
        }

        // Load PDF, set page background, and save
        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                // Pages are 1‑based
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.Background = bgColor;
                }

                doc.Save(outputPdfPath); // PDF format, no SaveOptions needed
            }

            Console.WriteLine($"PDF saved with '{config?.Theme}' theme background to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}