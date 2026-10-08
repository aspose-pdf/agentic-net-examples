using System;
using System.IO;
using Aspose.Pdf;

class UpdateXmpProperty
{
    static void Main(string[] args)
    {
        // Expected arguments: inputPdf outputPdf propertyName propertyValue
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: UpdateXmpProperty <input.pdf> <output.pdf> <propertyName> <propertyValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string propertyName = args[2];
        string propertyValue = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Copy the original PDF to the output location (modifications will be written here)
        File.Copy(inputPath, outputPath, true);

        // Load the PDF document
        Document pdfDoc = new Document(outputPath);

        // Update (or add) the XMP property using the Document.Metadata indexer.
        // If a custom namespace is required, register it before setting the value.
        // Example: pdfDoc.Metadata.RegisterNamespaceUri("my", "http://example.com/custom");
        pdfDoc.Metadata[propertyName] = propertyValue;

        // Save the PDF with the new metadata
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Property '{propertyName}' updated to '{propertyValue}' in '{outputPath}'.");
    }
}
