using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageExample
{
    static void Main()
    {
        // Paths for the source PDF, the new image, and the output PDF
        const string pdfPath      = "input.pdf";
        const string newImagePath = "newImage.jpg";
        const string outputPath   = "output.pdf";

        // Ensure source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Image not found: {newImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Choose the page that contains the image to replace (1‑based index)
            Page page = doc.Pages[1];

            // Replace the first image in the page's image collection.
            // XImageCollection uses 1‑based indexing for Replace(int, Stream).
            using (FileStream imgStream = File.OpenRead(newImagePath))
            {
                page.Resources.Images.Replace(1, imgStream);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image replaced and saved to '{outputPath}'.");
    }
}