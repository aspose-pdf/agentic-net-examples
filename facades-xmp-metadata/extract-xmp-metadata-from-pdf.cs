using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputXmlPath = "xmp_metadata.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the XMP metadata facade and retrieve the raw XML bytes
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);
            byte[] data = xmp.GetXmpMetadata(); // returns XML as byte[]

            // Convert the byte array to a UTF‑8 string
            string xml = Encoding.UTF8.GetString(data);

            // Save the XML string to a file (optional)
            File.WriteAllText(outputXmlPath, xml, Encoding.UTF8);
            Console.WriteLine($"XMP metadata saved to '{outputXmlPath}'.");
        }
    }
}