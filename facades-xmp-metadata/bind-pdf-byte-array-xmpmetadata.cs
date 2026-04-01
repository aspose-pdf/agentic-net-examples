using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputMetadataPath = "metadata.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a byte array
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Create a memory stream from the byte array and load it into a Document
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (Document pdfDocument = new Document(pdfStream))
        {
            // Bind the Document to PdfXmpMetadata facade
            PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
            xmpMetadata.BindPdf(pdfDocument);

            // Retrieve the XMP metadata as a byte array (XML format)
            byte[] metadataBytes = xmpMetadata.GetXmpMetadata();

            // Save the metadata to a file for inspection
            File.WriteAllBytes(outputMetadataPath, metadataBytes);
            Console.WriteLine($"XMP metadata extracted to '{outputMetadataPath}'.");
        }
    }
}