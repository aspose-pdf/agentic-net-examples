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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Retrieve existing XMP metadata
        byte[] existingMetadata = xmp.GetXmpMetadata();

        // If no metadata is present, add a minimal set
        if (existingMetadata == null || existingMetadata.Length == 0)
        {
            // Creator tool
            xmp.Add(DefaultMetadataProperties.CreatorTool, new XmpValue("Aspose.Pdf for .NET"));
            // Creation date
            XmpValue createDate = new XmpValue(DateTime.UtcNow);
            xmp.Add(DefaultMetadataProperties.CreateDate, createDate);
            // Modification date
            XmpValue modifyDate = new XmpValue(DateTime.UtcNow);
            xmp.Add(DefaultMetadataProperties.ModifyDate, modifyDate);
        }

        // Save the PDF with the (new) metadata
        xmp.Save(outputPath);
        xmp.Close();

        Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
    }
}