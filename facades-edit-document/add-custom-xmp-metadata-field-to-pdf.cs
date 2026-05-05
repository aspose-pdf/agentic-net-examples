using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF into the XMP metadata facade, add the custom field, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);                 // Load PDF
            xmp.Add("ProjectID", "12345");           // Add custom XMP metadata
            xmp.Save(outputPath);                    // Save PDF with updated XMP
        }

        // Verify that the custom field is also visible in the document's info dictionary.
        using (PdfFileInfo info = new PdfFileInfo(outputPath))
        {
            info.BindPdf(outputPath);
            string projectId = info.GetMetaInfo("ProjectID");
            Console.WriteLine($"ProjectID in document properties: {projectId}");
        }
    }
}