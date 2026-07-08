using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output.pdf";
        const string placeholderImgPath = "placeholder.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(placeholderImgPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImgPath}");
            return;
        }

        // Load placeholder image once into memory (preserves original dimensions when replaced)
        byte[] placeholderBytes = File.ReadAllBytes(placeholderImgPath);

        // Open the source PDF (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over odd‑numbered pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber += 2)
            {
                Page page = doc.Pages[pageNumber];
                var images = page.Resources.Images;

                // Replace each image on the current page with the placeholder
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Create a fresh stream for each replacement (Replace consumes the stream)
                    using (MemoryStream ms = new MemoryStream(placeholderBytes))
                    {
                        images.Replace(imgIndex, ms);
                    }
                }
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images on odd pages replaced. Output saved to '{outputPdfPath}'.");
    }
}