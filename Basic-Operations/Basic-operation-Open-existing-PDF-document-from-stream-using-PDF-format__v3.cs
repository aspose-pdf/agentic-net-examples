using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream stream = File.OpenRead(inputPath))
        {
            // Load the PDF document from the stream (Document implements IDisposable)
            using (Document doc = new Document(stream))
            {
                // Example operation: output the number of pages
                Console.WriteLine($"Pages in document: {doc.Pages.Count}");

                // Save the document back to disk in PDF format
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}