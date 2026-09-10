using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfXmpMetadata facade and bind it to the PDF file.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Retrieve the entire XMP metadata as a byte array.
            byte[] metadataBytes = xmp.GetXmpMetadata();

            // Example output: display the size of the retrieved metadata.
            Console.WriteLine($"XMP metadata size: {metadataBytes.Length} bytes");
        }
    }
}