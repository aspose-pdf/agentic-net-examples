using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Edit standard metadata fields via Document.Info
        // These fields are part of the PDF's accessibility information
        doc.Info.Title = "Updated Document Title";
        doc.Info.Author = "Jane Doe";
        doc.Info.Subject = "Accessibility Metadata Update";
        doc.Info.Keywords = "accessibility, PDF, metadata, tags";
        doc.Info.Creator = "My Accessibility Tool";

        // Save the modified PDF (uses the provided document-save rule)
        doc.Save(outputPath);

        Console.WriteLine($"PDF metadata updated and saved to: {outputPath}");
    }
}