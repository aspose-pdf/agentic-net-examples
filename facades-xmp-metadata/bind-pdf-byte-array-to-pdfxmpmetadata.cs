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
        using (var tempDoc = new Document())
        {
            // Add a blank page (or any content you need).
            tempDoc.Pages.Add();

            // Save the temporary document to a MemoryStream and capture the byte array.
            using (var ms = new MemoryStream())
            {
                tempDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Load the PDF bytes into an Aspose.Pdf.Document via a MemoryStream.
        // ------------------------------------------------------------
        using (var pdfStream = new MemoryStream(pdfBytes))
        using (var pdfDoc = new Document(pdfStream))
        {
            // ------------------------------------------------------------
            // 3. Bind the in‑memory Document to the PdfXmpMetadata facade.
            // ------------------------------------------------------------
            var xmpMetadata = new PdfXmpMetadata();
            xmpMetadata.BindPdf(pdfDoc);

            // ------------------------------------------------------------
            // 4. Retrieve the XMP metadata as an XML byte array.
            // ------------------------------------------------------------
            byte[] xmpData = xmpMetadata.GetXmpMetadata();

            // ------------------------------------------------------------
            // 5. (Optional) Display the metadata – here we write it to the console.
            // ------------------------------------------------------------
            string xmpXml = System.Text.Encoding.UTF8.GetString(xmpData);
            Console.WriteLine("--- XMP Metadata (XML) ---");
            Console.WriteLine(xmpXml);
        }
    }
}
