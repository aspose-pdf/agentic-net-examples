using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(pdfPath);

        // Retrieve the raw XMP metadata XML string using the PdfXmpMetadata facade.
        string xmpXml;
        using (PdfXmpMetadata xmp = new PdfXmpMetadata(pdfDoc))
        {
            // GetXmpMetadata returns a byte[]; convert it to a UTF‑8 string.
            byte[] xmpBytes = xmp.GetXmpMetadata();
            xmpXml = (xmpBytes != null && xmpBytes.Length > 0) ? Encoding.UTF8.GetString(xmpBytes) : string.Empty;
        }

        if (string.IsNullOrEmpty(xmpXml))
        {
            Console.WriteLine("No XMP metadata found in the PDF.");
        }
        else
        {
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xmpXml);
        }
    }
}
