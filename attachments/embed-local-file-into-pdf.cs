using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourceFilePath = "sample.txt";      // local file to embed
        const string outputPdfPath = "output.pdf";       // resulting PDF

        if (!File.Exists(sourceFilePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourceFilePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page so the PDF is not empty
            doc.Pages.Add();

            // Create a FileSpecification using the constructor that accepts the file path and a description
            FileSpecification fileSpec = new FileSpecification(sourceFilePath, "Embedded sample text file");
            // Optional: set the modification date of the attachment
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the FileSpecification to the document's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF; the embedded file will be part of the output
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded file saved to '{outputPdfPath}'.");
    }
}