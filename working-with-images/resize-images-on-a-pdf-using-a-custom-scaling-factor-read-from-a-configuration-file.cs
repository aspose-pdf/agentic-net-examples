using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output PDF and configuration file
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "scale.txt";

        // Validate files exist
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

        // Read scaling factor from configuration file (expects a single numeric value)
        double scalingFactor = 1.0;
        string configContent = File.ReadAllText(configPath).Trim();

        if (!double.TryParse(configContent, out scalingFactor) || scalingFactor <= 0)
        {
            Console.Error.WriteLine("Invalid scaling factor in configuration. Using default factor = 1.0");
            scalingFactor = 1.0;
        }

        // Load the PDF document (using the required lifecycle rule)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Iterate through all pages
            foreach (Page page in pdfDocument.Pages)
            {
                // Iterate through all paragraph objects on the page
                // Paragraph collections are 1‑based indexed
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // Identify Image objects (derived from BaseParagraph)
                    if (page.Paragraphs[i] is Image img)
                    {
                        // Apply the custom scaling factor
                        img.ImageScale = scalingFactor;
                    }
                }
            }

            // Save the modified PDF (using the required lifecycle rule)
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Images resized with factor {scalingFactor} and saved to '{outputPdfPath}'.");
    }
}