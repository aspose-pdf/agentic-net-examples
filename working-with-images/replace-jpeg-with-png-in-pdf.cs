using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string pngReplacement = "replacement.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pngReplacement))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {pngReplacement}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Access the image collection of the page
                var images = page.Resources.Images;

                // Replace each image with the PNG while preserving its position
                for (int i = 1; i <= images.Count; i++)
                {
                    // Open the PNG stream for each replacement
                    using (FileStream pngStream = File.OpenRead(pngReplacement))
                    {
                        images.Replace(i, pngStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with PNG images to '{outputPdf}'.");
    }
}