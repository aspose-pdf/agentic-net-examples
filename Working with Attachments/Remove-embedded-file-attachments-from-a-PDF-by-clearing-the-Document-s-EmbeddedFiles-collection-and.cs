using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all embedded file attachments from the document
            doc.EmbeddedFiles.Delete();

            // Save the cleaned document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Embedded files removed. Saved to '{outputPath}'.");
    }
}