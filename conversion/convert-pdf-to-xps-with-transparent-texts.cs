using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Configure XPS save options and enable transparent text preservation
                XpsSaveOptions xpsOptions = new XpsSaveOptions
                {
                    SaveTransparentTexts = true
                };

                // Save as XPS using the explicit SaveOptions subclass
                doc.Save(outputPath, xpsOptions);
            }

            Console.WriteLine($"PDF successfully saved as XPS with transparent texts to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}