using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Simple POCO to map the configuration file
    private class AppSettings
    {
        public double ImageScaleFactor { get; set; }
    }

    static void Main()
    {
        // Load scaling factor from appsettings.json without using Microsoft.Extensions.Configuration
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
            var settings = JsonSerializer.Deserialize<AppSettings>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (settings == null)
                throw new InvalidOperationException("Failed to deserialize configuration.");
            scaleFactor = settings.ImageScaleFactor;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all paragraph elements on the page (1‑based collection)
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // Identify Image objects and apply the scaling factor
                    if (page.Paragraphs[i] is Image img)
                    {
                        img.ImageScale = scaleFactor; // Resize image proportionally
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Images resized by a factor of {scaleFactor} and saved to '{outputPath}'.");
    }
}
