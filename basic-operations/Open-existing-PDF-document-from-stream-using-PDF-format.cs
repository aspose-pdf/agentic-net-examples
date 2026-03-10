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

        // Open the PDF document from a FileStream.
        // The using statements ensure deterministic disposal of both the stream and the Document.
        using (FileStream stream = File.OpenRead(inputPath))
        using (Document doc = new Document(stream))
        {
            // Example operation: output the number of pages (1‑based indexing).
            Console.WriteLine($"Page count: {doc.Pages.Count}");

            // Save the opened document to a new file (optional).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}