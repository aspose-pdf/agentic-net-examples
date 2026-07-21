using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for configuration, input PDF and output PDF
        const string configPath = "scale.txt";
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_resized.pdf";

        // Validate configuration file
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Validate input PDF
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Read scaling factor from configuration (expects a single double value)
        if (!double.TryParse(File.ReadAllText(configPath).Trim(), out double scaleFactor) || scaleFactor <= 0)
        {
            Console.Error.WriteLine("Invalid scaling factor in configuration file.");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all paragraphs on the page
                for (int paraIndex = 1; paraIndex <= page.Paragraphs.Count; paraIndex++)
                {
                    // Check if the paragraph is an Image object
                    if (page.Paragraphs[paraIndex] is Image img)
                    {
                        // Apply the custom scaling factor
                        img.ImageScale = scaleFactor;
                    }
                }
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images resized by a factor of {scaleFactor} and saved to '{outputPdf}'.");
    }
}