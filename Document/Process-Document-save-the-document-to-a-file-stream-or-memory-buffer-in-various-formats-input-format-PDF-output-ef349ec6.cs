using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Save the document to a file (output PDF)
        pdfDocument.Save(outputPath);

        // Additionally, save the document to a memory buffer
        using (MemoryStream memoryStream = new MemoryStream())
        {
            pdfDocument.Save(memoryStream);
            // Example: write the buffer to another file
            File.WriteAllBytes("output_from_stream.pdf", memoryStream.ToArray());
        }

        Console.WriteLine("PDF document processed and saved successfully.");
    }
}