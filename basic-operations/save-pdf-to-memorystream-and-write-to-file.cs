using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and related types

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Path where the final PDF file will be written from the memory stream
        const string outputPdfPath = "output_from_memory.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document, process it, and save to a MemoryStream
        using (Document pdfDoc = new Document(inputPdfPath))          // Load document (lifecycle rule)
        using (MemoryStream memStream = new MemoryStream())          // Create in‑memory stream
        {
            // Save the document into the memory stream (Document.Save(Stream) overload)
            pdfDoc.Save(memStream);

            // Reset the stream position to the beginning before reading
            memStream.Position = 0;

            // Write the contents of the memory stream to a physical file
            using (FileStream fileStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
            {
                memStream.CopyTo(fileStream);
            }
        }

        Console.WriteLine($"PDF saved to memory and written to file: {outputPdfPath}");
    }
}