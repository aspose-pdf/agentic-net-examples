using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        // Paths for the source PDF (as a file) and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Desired new title for the PDF document
        const string newTitle = "My New Title";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Read the entire PDF file into a byte array
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Load the PDF from the byte array using a MemoryStream
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        {
            // Create a Document instance from the stream
            using (Document doc = new Document(ms))
            {
                // Update the title metadata (DocumentInfo.Title)
                doc.Info.Title = newTitle;

                // Save the modified document to disk
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Title updated and saved to '{outputPath}'.");
    }
}
