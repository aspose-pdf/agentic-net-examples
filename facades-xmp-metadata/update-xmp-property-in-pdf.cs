using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: inputPdf outputPdf xmpKey xmpValue
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <outputPdf> <xmpKey> <xmpValue>");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string xmpKey     = args[2];
        string xmpValue   = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF and modify its XMP metadata
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPath);               // Load the PDF
                xmp.Add(xmpKey, xmpValue);            // Set or update the XMP property
                xmp.Save(outputPath);                 // Save the updated PDF
            }

            Console.WriteLine($"XMP property '{xmpKey}' updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}