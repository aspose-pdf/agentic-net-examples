using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Example: load a PDF file into a byte array (replace with your own source)
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        RotatePageFive(pdfBytes, outputPath);
        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }

    static void RotatePageFive(byte[] pdfData, string outputPath)
    {
        // Load the PDF from the byte array using a MemoryStream
        using (MemoryStream ms = new MemoryStream(pdfData))
        using (Document doc = new Document(ms))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count >= 5)
            {
                // Rotate page 5 by 180 degrees
                doc.Pages[5].Rotate = Rotation.on180;
            }
            else
            {
                Console.Error.WriteLine("The document has fewer than 5 pages; rotation skipped.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }
    }
}