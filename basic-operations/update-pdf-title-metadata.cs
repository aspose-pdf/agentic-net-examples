using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF in memory (or replace this with your own byte[] source).
        byte[] pdfBytes = CreateSamplePdf();

        const string outputPath = "output.pdf";

        // Load the PDF from the byte array, modify metadata, and save.
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(pdfStream))
        {
            // Update the document's title metadata.
            doc.SetTitle("Updated PDF Title");

            // Save the modified PDF to disk.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }

    // Helper that creates a minimal PDF and returns its bytes.
    private static byte[] CreateSamplePdf()
    {
        using (var ms = new MemoryStream())
        {
            // Create a new document with a single blank page.
            var doc = new Document();
            doc.Pages.Add();
            doc.Save(ms);
            return ms.ToArray();
        }
    }
}