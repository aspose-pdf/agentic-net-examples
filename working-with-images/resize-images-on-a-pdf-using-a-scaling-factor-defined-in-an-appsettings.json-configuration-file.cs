using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to configuration, input PDF and output PDF
        const string configPath = "appsettings.json";
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_resized.pdf";

        // Verify required files exist
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Read scaling factor from appsettings.json (e.g., { "ImageScaleFactor": 0.5 })
        double scaleFactor = 1.0; // default – no scaling
        using (FileStream cfgStream = File.OpenRead(configPath))
        using (JsonDocument jsonDoc = JsonDocument.Parse(cfgStream))
        {
            if (jsonDoc.RootElement.TryGetProperty("ImageScaleFactor", out JsonElement factorElem) &&
                factorElem.TryGetDouble(out double factorValue))
            {
                scaleFactor = factorValue;
            }
        }

        // Load the PDF, resize images, and save the result
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate over all pages
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate over all paragraph elements on the page
                foreach (var paragraph in page.Paragraphs)
                {
                    // Identify Image objects and apply the scaling factor
                    if (paragraph is Image img)
                    {
                        img.ImageScale = scaleFactor;
                    }
                }
            }

            // Save the modified PDF (Document.Save(string) writes PDF regardless of extension)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Images resized by a factor of {scaleFactor} and saved to '{outputPdf}'.");
    }
}