using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF (replace with your actual file path)
        const string sourcePdfPath = "source.pdf";

        // Ensure the source file exists before proceeding
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"File not found: {sourcePdfPath}");
            return;
        }

        // Load the PDF document using the high‑level Document API.
        Document pdfDocument = new Document(sourcePdfPath);

        // Example modification: rotate the first page 90 degrees clockwise.
        // The Rotate property expects a value from the Rotation enum.
        if (pdfDocument.Pages.Count >= 1)
        {
            pdfDocument.Pages[1].Rotate = Rotation.on90; // corrected enum usage
        }

        // Save the modified PDF into a MemoryStream (no disk I/O).
        using (MemoryStream outputStream = new MemoryStream())
        {
            pdfDocument.Save(outputStream);

            // At this point, outputStream contains the PDF bytes.
            // Reset the position if you need to read from the beginning.
            outputStream.Position = 0;

            // Example: write the stream length to console.
            Console.WriteLine($"Modified PDF saved to MemoryStream ({outputStream.Length} bytes).");
        }
    }
}