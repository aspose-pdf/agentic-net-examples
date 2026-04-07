using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        using (Document doc = new Document(inputPath))
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);
            // Register a custom namespace for the project schema
            xmp.RegisterNamespaceURI("proj", "http://example.com/project");
            // Add custom elements under the new namespace
            xmp.Add("proj:Identifier", "PRJ-001");
            xmp.Add("proj:Version", "1.2.3");
            // Save the PDF with updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Updated XMP metadata saved to '{outputPath}'.");
    }
}