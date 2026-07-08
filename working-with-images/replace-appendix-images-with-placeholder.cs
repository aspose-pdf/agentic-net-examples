using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string placeholderPath = "placeholder.jpg";
        const int appendixStartPage = 5; // first page of the appendix (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Load placeholder image once into a memory stream (JPEG required)
            using (FileStream placeholderStream = File.OpenRead(placeholderPath))
            {
                // Iterate over all pages that belong to the appendix
                for (int pageIndex = appendixStartPage; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    var images = page.Resources.Images; // XImageCollection

                    // Replace each image on the page with the placeholder
                    for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                    {
                        // Reset stream position before each replace call
                        placeholderStream.Position = 0;
                        images.Replace(imgIndex, placeholderStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Protected PDF saved to '{outputPath}'.");
    }
}