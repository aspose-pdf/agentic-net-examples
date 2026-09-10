using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "scale.cfg"; // file containing a single double value

        // Read scaling factor from configuration file
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        if (!double.TryParse(File.ReadAllText(configPath).Trim(), out double scaleFactor) || scaleFactor <= 0)
        {
            Console.Error.WriteLine("Invalid scaling factor in configuration file.");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate through all paragraph elements on the page
                for (int paraIndex = 1; paraIndex <= page.Paragraphs.Count; paraIndex++)
                {
                    // Check if the paragraph is an Image
                    if (page.Paragraphs[paraIndex] is Image img)
                    {
                        // Apply the custom scaling factor
                        img.ImageScale = scaleFactor;
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images resized with factor {scaleFactor} and saved to '{outputPdfPath}'.");
    }
}