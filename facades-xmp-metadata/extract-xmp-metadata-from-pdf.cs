using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // path to the PDF file
        const string outPath = "xmp.xml";     // where to store the raw XMP XML

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF and retrieve its XMP metadata as a byte array
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);
            byte[] rawData = xmp.GetXmpMetadata();   // XML bytes

            // Convert the bytes to a UTF‑8 string
            string xml = Encoding.UTF8.GetString(rawData);

            // Save the XML string to a file (optional)
            File.WriteAllText(outPath, xml, Encoding.UTF8);
            Console.WriteLine($"XMP metadata extracted to '{outPath}'.");
        }
    }
}