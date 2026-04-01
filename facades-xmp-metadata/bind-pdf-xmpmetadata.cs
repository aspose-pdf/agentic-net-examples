using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a new PdfXmpMetadata instance
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
        // Bind the PDF file to the facade
        xmpMetadata.BindPdf(inputPath);

        // Retrieve the XMP metadata as a byte array
        byte[] xmpBytes = xmpMetadata.GetXmpMetadata();
        Console.WriteLine($"XMP metadata size: {xmpBytes.Length} bytes");

        // Clean up resources
        xmpMetadata.Close();
    }
}