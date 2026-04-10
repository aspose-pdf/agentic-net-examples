using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment

class PdfXmpMetadataExample
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a simple PDF document in memory (no external file).
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var tempStream = new MemoryStream())
        {
            // Create a new PDF document.
            var tempDoc = new Document();
            // Add a page with a single paragraph of text.
            var page = tempDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello, Aspose.Pdf!"));
            // Save the document to the temporary stream.
            tempDoc.Save(tempStream);
            // Extract the byte array.
            pdfBytes = tempStream.ToArray();
        }

        // ------------------------------------------------------------
        // 2. Load the PDF from the byte array using a MemoryStream.
        // ------------------------------------------------------------
        using (var pdfStream = new MemoryStream(pdfBytes))
        using (var pdfDoc = new Document(pdfStream))
        {
            // --------------------------------------------------------
            // 3. Bind the in‑memory PDF document to the XMP metadata facade.
            // --------------------------------------------------------
            var xmpMetadata = new PdfXmpMetadata();
            xmpMetadata.BindPdf(pdfDoc);

            // --------------------------------------------------------
            // 4. Retrieve the XMP metadata as a byte array (XML format).
            // --------------------------------------------------------
            byte[] xmpBytes = xmpMetadata.GetXmpMetadata();

            // --------------------------------------------------------
            // 5. (Optional) Save the metadata to a file for inspection.
            // --------------------------------------------------------
            File.WriteAllBytes("output_metadata.xml", xmpBytes);

            Console.WriteLine("XMP metadata extracted and saved to 'output_metadata.xml'.");
        }
    }
}