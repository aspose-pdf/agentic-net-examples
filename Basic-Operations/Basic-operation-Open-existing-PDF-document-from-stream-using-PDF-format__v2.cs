using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Load the PDF document from the stream
            using (Document doc = new Document(stream))
            {
                // Example operation: display page count
                Console.WriteLine($"Page count: {doc.Pages.Count}");

                // Save the document to a new file (PDF format)
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
    }
}