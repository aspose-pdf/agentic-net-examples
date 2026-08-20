using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Rotate the first page 90 degrees clockwise (if a page exists)
        if (pdfDoc.Pages.Count >= 1)
        {
            // Use the Rotation enum; integer literals are not allowed.
            pdfDoc.Pages[1].Rotate = Rotation.on90;
        }

        // Save the modified PDF into a MemoryStream (no disk I/O)
        using (MemoryStream ms = new MemoryStream())
        {
            pdfDoc.Save(ms);
            ms.Position = 0; // reset for downstream consumers

            Console.WriteLine($"Modified PDF saved to MemoryStream, length = {ms.Length} bytes");
            // The MemoryStream can now be returned, sent over a network, etc.
        }
    }
}