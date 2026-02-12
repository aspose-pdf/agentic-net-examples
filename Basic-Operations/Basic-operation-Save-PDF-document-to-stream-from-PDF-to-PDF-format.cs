using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Load the PDF document from the file
        Document pdfDocument = new Document(inputPath);

        // Save the document to a memory stream (PDF format)
        using (MemoryStream stream = new MemoryStream())
        {
            pdfDocument.Save(stream); // Save to stream

            // Reset stream position for further reading if needed
            stream.Position = 0;

            // Example: write the stream contents to an output file
            const string outputPath = "output.pdf";
            File.WriteAllBytes(outputPath, stream.ToArray());

            Console.WriteLine($"PDF successfully saved to stream and written to '{outputPath}'.");
        }
    }
}