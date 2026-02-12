using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string sourcePath = "input.pdf";
        // Path to the output PDF file (just to demonstrate that the document was loaded)
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream pdfStream = File.OpenRead(sourcePath))
        {
            // Load the PDF document from the stream
            Document pdfDocument = new Document(pdfStream);

            // Perform any desired operations on the document here
            // For this example we simply save it to a new file

            // Save the document (uses the provided document-save rule)
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully loaded from stream and saved to '{outputPath}'.");
    }
}