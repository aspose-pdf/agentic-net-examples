using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    // Simple POCO to map the configuration file
    private class AppConfig
    {
        public double ImageScaleFactor { get; set; }
    }

    static void Main()
    {
        // Load scaling factor from appsettings.json using System.Text.Json
        const string configPath = "appsettings.json";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        double scaleFactor;
        try
        {
            string json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<AppConfig>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (config == null)
            {
                Console.Error.WriteLine("Failed to deserialize configuration.");
                return;
            }
            scaleFactor = config.ImageScaleFactor;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate all paragraphs on the page
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // Identify Image paragraphs
                    if (page.Paragraphs[i] is Aspose.Pdf.Image img)
                    {
                        // Apply the scaling factor (ImageScale property)
                        img.ImageScale = scaleFactor;
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
