using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfXmpMetadataExample
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a simple PDF document entirely in memory.
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var doc = new Document())
        {
            // Add a blank page (or any content you need).
            doc.Pages.Add();

            // Save the document to a MemoryStream and capture the byte array.
            using (var tempStream = new MemoryStream())
            {
                doc.Save(tempStream);
                pdfBytes = tempStream.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Bind the PDF byte array to the PdfXmpMetadata facade.
        // ------------------------------------------------------------
        using (var pdfStream = new MemoryStream(pdfBytes))
        {
            var xmpMetadata = new PdfXmpMetadata();
            xmpMetadata.BindPdf(pdfStream);

            // --------------------------------------------------------
            // 3. Retrieve the XMP metadata (XML) as a byte array.
            // --------------------------------------------------------
            byte[] xmpData = xmpMetadata.GetXmpMetadata();

            // Optional: display the size or content of the metadata.
            Console.WriteLine($"XMP metadata extracted – length: {xmpData.Length} bytes");
            // If you want to see the XML as text:
            // Console.WriteLine(System.Text.Encoding.UTF8.GetString(xmpData));
        }
    }
}
