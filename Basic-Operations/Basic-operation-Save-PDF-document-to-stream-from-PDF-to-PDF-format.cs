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
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document from the file system
        Document pdfDocument = new Document(inputPath);

        // Save the document into a memory stream (PDF format)
        using (MemoryStream stream = new MemoryStream())
        {
            // The Save method writes the PDF bytes into the provided stream
            pdfDocument.Save(stream);

            // Example: write the stream contents to a physical file
            File.WriteAllBytes("output.pdf", stream.ToArray());

            Console.WriteLine("PDF successfully saved to a stream and written to output.pdf");
        }
    }
}