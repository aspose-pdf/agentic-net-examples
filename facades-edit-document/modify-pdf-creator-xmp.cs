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

        // Retrieve existing XMP metadata
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);
        byte[] originalMetadata = xmp.GetXmpMetadata();
        Console.WriteLine($"Original XMP size: {originalMetadata.Length} bytes");

        // Modify the Creator field using PdfFileInfo
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(inputPath);
        Console.WriteLine($"Original Creator: {fileInfo.Creator}");
        fileInfo.Creator = "My New Creator";

        // Save the PDF with updated XMP metadata
        bool success = fileInfo.SaveNewInfoWithXmp(outputPath);
        Console.WriteLine(success ? $"Saved updated PDF to {outputPath}" : "Failed to save updated PDF");
    }
}