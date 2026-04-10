using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: inputPdf outputPdf xmpPropertyName newValue
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <exe> <input.pdf> <output.pdf> <xmpProperty> <newValue>");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string xmpKey     = args[2];
        string xmpValue   = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Update (or add) the specified XMP property
        // The Add method replaces the value if the key already exists
        xmp.Add(xmpKey, xmpValue);

        // Save the PDF with the updated XMP metadata
        xmp.Save(outputPath);

        Console.WriteLine($"XMP property '{xmpKey}' updated and saved to '{outputPath}'.");
    }
}