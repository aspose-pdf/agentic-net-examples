using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfXmpMetadata

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: inputPdf outputPdf xmpKey xmpValue
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <input.pdf> <output.pdf> <xmpKey> <xmpValue>");
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

        try
        {
            // Bind the PDF and manipulate its XMP metadata
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPath);

                // Add or update the specified XMP property.
                // PdfXmpMetadata provides an overload that accepts a plain string value,
                // eliminating the need for the Aspose.Pdf.Xmp.XmpValue type.
                xmp.Add(xmpKey, xmpValue);

                // Save the PDF with the updated XMP metadata
                xmp.Save(outputPath);
            }

            Console.WriteLine($"XMP property '{xmpKey}' updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
