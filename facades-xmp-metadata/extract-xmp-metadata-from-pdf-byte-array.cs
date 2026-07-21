using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class PdfXmpMetadataExample
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF document entirely in memory.
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var doc = new Document())
        {
            // Add a single page.
            var page = doc.Pages.Add();
            // Add a simple text fragment so the PDF is not empty.
            page.Paragraphs.Add(new TextFragment("Hello, Aspose.PDF!"));

            // Save the document to a MemoryStream and capture the byte array.
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Bind the PDF byte array to the PdfXmpMetadata facade.
        // ------------------------------------------------------------
        using (var pdfStream = new MemoryStream(pdfBytes))
        {
            var xmp = new PdfXmpMetadata();
            xmp.BindPdf(pdfStream);

            // --------------------------------------------------------
            // 3. Retrieve the XMP metadata as an XML byte array.
            // --------------------------------------------------------
            byte[] xmpData = xmp.GetXmpMetadata();

            // --------------------------------------------------------
            // 4. (Optional) Save the metadata to a file for inspection.
            // --------------------------------------------------------
            File.WriteAllBytes("metadata.xml", xmpData);
            Console.WriteLine("XMP metadata extracted and saved to metadata.xml");
        }
    }
}