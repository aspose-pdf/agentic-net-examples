using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "xmp.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF and retrieve the XMP metadata as a byte array
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);
            byte[] data = xmp.GetXmpMetadata();

            // Convert the byte array to a UTF‑8 string
            string xml = Encoding.UTF8.GetString(data);

            // Save the raw XMP XML to a file
            File.WriteAllText(outputPath, xml, Encoding.UTF8);
            Console.WriteLine($"XMP metadata saved to '{outputPath}'.");
        }
    }
}