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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove all embedded file attachments, if any
            // EmbeddedFiles is an EmbeddedFileCollection; Delete() removes all entries
            doc.EmbeddedFiles.Delete();

            // Save the modified document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachments removed. Saved to '{outputPath}'.");
    }
}