using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Load configuration from appsettings.json using System.Text.Json (no external config packages required)
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
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;
            if (root.TryGetProperty("ImageScaleFactor", out JsonElement factorElem) && factorElem.TryGetDouble(out double factor))
            {
                scaleFactor = factor;
            }
            else
            {
                Console.Error.WriteLine("ImageScaleFactor not found or invalid in configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        const string inputPdf = "input.pdf";
        const string outputPdf = "output_resized.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, modify images, and save
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all paragraphs on the page
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // Check if the paragraph is an Image
                    if (page.Paragraphs[i] is Image img)
                    {
                        // Apply the scaling factor (ImageScale scales both width and height proportionally)
                        img.ImageScale = scaleFactor;
                    }
                }
            }

            // Save the modified PDF (format inferred from file extension)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images resized with factor {scaleFactor} and saved to '{outputPdf}'.");
    }
}
