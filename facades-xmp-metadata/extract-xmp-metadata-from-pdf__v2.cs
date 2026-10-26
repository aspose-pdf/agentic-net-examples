using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

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

        // Load the PDF document
        Document pdfDoc = new Document(pdfPath);

        // Retrieve the raw XMP metadata as a byte array using GetXmpMetadata(Stream)
        byte[] xmpBytes;
        using (MemoryStream ms = new MemoryStream())
        {
            // The current Aspose.PDF API writes the XMP data into the provided stream
            pdfDoc.GetXmpMetadata(ms);
            xmpBytes = ms.ToArray();
        }

        if (xmpBytes == null || xmpBytes.Length == 0)
        {
            Console.WriteLine("No XMP metadata found in the PDF.");
        }
        else
        {
            // Convert the byte array to a UTF‑8 string
            string xmpXml = Encoding.UTF8.GetString(xmpBytes);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xmpXml);
        }
    }
}
