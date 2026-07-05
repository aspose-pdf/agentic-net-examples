using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageExample
{
    static void Main()
    {
        // Paths to the source PDF, the new image file, and the output PDF.
        const string pdfPath      = "input.pdf";
        const string newImagePath = "newImage.jpg";
        const string outputPath   = "output.pdf";

        // Ensure the source files exist.
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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Choose the page that contains the image to replace (1‑based index).
            Page page = doc.Pages[1];

            // The Images collection is 1‑based as well.
            // Replace the first image in the collection with the new image stream.
            // Adjust the index if you need to replace a different image.
            using (FileStream imgStream = File.OpenRead(newImagePath))
            {
                // Replace image at index 1.
                page.Resources.Images.Replace(1, imgStream);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image replaced and saved to '{outputPath}'.");
    }
}