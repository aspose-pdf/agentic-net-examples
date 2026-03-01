using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        // Load the document from the stream
        using (Document pdfDoc = new Document(inputStream))
        // Create an output stream where the PDF will be written
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Save the document to the output stream (PDF format by default)
            pdfDoc.Save(outputStream);
        }

        Console.WriteLine($"PDF successfully saved to stream and written to '{outputPath}'.");
    }
}