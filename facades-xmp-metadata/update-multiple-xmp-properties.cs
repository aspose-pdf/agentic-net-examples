using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Add or update multiple XMP properties.
            // Using the enum for standard properties.
            xmp.Add(DefaultMetadataProperties.CreatorTool, "MyApplication");
            xmp.Add(DefaultMetadataProperties.CreateDate, DateTime.UtcNow);
            xmp.Add(DefaultMetadataProperties.ModifyDate, DateTime.UtcNow);
            xmp.Add(DefaultMetadataProperties.Nickname, "SampleDocument");

            // Add a custom XMP property (any string key is allowed).
            xmp.Add("xmp:CustomProperty", "CustomValue");

            // Save all changes in one atomic operation.
            // The Save method writes the updated PDF with the new XMP metadata.
            xmp.Save(outputPath);
        }

        Console.WriteLine($"XMP metadata updated and saved to '{outputPath}'.");
    }
}