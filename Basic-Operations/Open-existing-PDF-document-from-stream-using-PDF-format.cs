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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream stream = File.OpenRead(inputPath))
        {
            // Load the PDF document from the stream
            using (Document doc = new Document(stream))
            {
                // Example operation: display the number of pages
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Save the document back to a file (PDF format)
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}