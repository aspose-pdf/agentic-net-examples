using System;
using System.IO;
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

        // Add custom XMP metadata field "ProjectID" with value "12345"
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF into the XMP facade
            xmp.BindPdf(inputPath);
            // Add the custom metadata entry
            xmp.Add("ProjectID", "12345");
            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        // Verify that the custom metadata appears in document properties
        using (PdfFileInfo info = new PdfFileInfo(outputPath))
        {
            string projectId = info.GetMetaInfo("ProjectID");
            Console.WriteLine($"ProjectID metadata: {projectId}");
        }
    }
}