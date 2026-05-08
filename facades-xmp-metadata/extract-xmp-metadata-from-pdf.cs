using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a new PdfXmpMetadata facade instance
        PdfXmpMetadata xmp = new PdfXmpMetadata();

        // Bind the PDF file to the facade
        xmp.BindPdf(inputPdf);

        // Retrieve the entire XMP metadata as an XML byte array
        byte[] xmpData = xmp.GetXmpMetadata();

        // Save the XML representation for inspection
        File.WriteAllBytes("output_xmp.xml", xmpData);

        // Release resources held by the facade
        xmp.Close();
    }
}