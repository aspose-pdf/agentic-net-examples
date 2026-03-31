using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "merged_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load and modify standard PDF file information
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(inputPath);
        fileInfo.Title = "Merged Metadata Document";
        fileInfo.Author = "John Doe";
        fileInfo.Subject = "Demonstration of XMP and PdfFileInfo merge";

        // Load XMP metadata and add custom entries
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);
        xmp.Add("xmp:CreatorTool", "Aspose.Pdf for .NET");
        xmp.Add("dc:description", "Document with combined PdfFileInfo and XMP metadata");

        // Save the PDF with merged metadata (PdfFileInfo + XMP)
        bool success = fileInfo.SaveNewInfoWithXmp(outputPath);
        if (success)
        {
            Console.WriteLine("PDF saved with merged metadata to '" + outputPath + "'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to save PDF with merged metadata.");
        }

        // Clean up facades
        xmp.Close();
        fileInfo.Close();
    }
}
