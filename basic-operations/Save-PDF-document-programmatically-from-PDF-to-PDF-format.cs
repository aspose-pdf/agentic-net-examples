using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and all related types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "output.pdf";  // destination PDF file

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Save the document as PDF. No SaveOptions are required because the
            // target format is PDF (the default).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
    }
}