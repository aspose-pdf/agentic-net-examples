using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText = "Descriptive alternate text for the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page in the document
            foreach (Page page in doc.Pages)
            {
                // Iterate over each image resource on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Set the alternate text for the image; returns true if successful
                    bool result = img.TrySetAlternativeText(altText, page);
                    Console.WriteLine($"Page {page.Number}: Image alt text set = {result}");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated image alternate text to '{outputPath}'.");
    }
}