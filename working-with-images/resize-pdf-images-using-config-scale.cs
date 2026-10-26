using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "resized.pdf";
        const string configPath = "appsettings.json";

        // Verify files exist
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

        // Load scaling factor from appsettings.json using System.Text.Json (no Microsoft.Extensions.Configuration dependency)
        double scalingFactor;
        try
        {
            string json = File.ReadAllText(configPath);
            using JsonDocument doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("ImageScalingFactor", out JsonElement factorElem) ||
                !factorElem.TryGetDouble(out scalingFactor) ||
                scalingFactor <= 0)
            {
                Console.Error.WriteLine("Invalid or missing ImageScalingFactor in configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Open PDF, resize each image, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Images that appear on a page are represented by Aspose.Pdf.Image objects
                // inside the page's Paragraphs collection. Adjust their ImageScale.
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Aspose.Pdf.Image imgParagraph)
                    {
                        // Apply scaling factor – ImageScale is a multiplier applied when the image is rendered.
                        imgParagraph.ImageScale = scalingFactor;
                    }
                }
            }

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images resized with factor {scalingFactor} and saved to '{outputPdfPath}'.");
    }
}
