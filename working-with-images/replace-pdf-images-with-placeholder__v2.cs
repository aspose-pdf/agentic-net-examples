using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output.pdf";         // result PDF
        const string placeholderImagePath = "placeholder.jpg"; // low‑resolution placeholder

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(placeholderImagePath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Read the placeholder image once into a byte array
            byte[] placeholderBytes = File.ReadAllBytes(placeholderImagePath);

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                var images = page.Resources.Images; // XImageCollection

                // Replace each image on the page with the placeholder
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Create a fresh stream for each replacement (Replace consumes the stream)
                    using (MemoryStream ms = new MemoryStream(placeholderBytes))
                    {
                        images.Replace(imgIndex, ms);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images replaced with placeholder. Saved to '{outputPdf}'.");
    }
}