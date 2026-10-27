using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists. If it does not, create a placeholder with at least three pages.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            // Add three blank pages so page index 3 is valid.
            placeholder.Pages.Add();
            placeholder.Pages.Add();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Load PDF bytes from the file.
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Load PDF from the byte array using a MemoryStream.
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            if (doc.Pages.Count >= 3)
            {
                // Rotate page 3 by 180 degrees.
                doc.Pages[3].Rotate = Rotation.on180;
            }
            else
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 rotated and saved to '{outputPath}'.");
    }
}
