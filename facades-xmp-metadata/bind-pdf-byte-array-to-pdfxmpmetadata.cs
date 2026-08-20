using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Example
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF in memory (so we don't depend on a file)
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var doc = new Document())
        {
            // Add a single blank page – enough for XMP binding demo
            doc.Pages.Add();

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Bind the PDF (provided as a byte array) to PdfXmpMetadata
        // ------------------------------------------------------------
        using var pdfStream = new MemoryStream(pdfBytes);
        // Ensure the stream is positioned at the beginning before binding
        pdfStream.Position = 0;

        var xmp = new PdfXmpMetadata();
        // BindPdf can accept a Stream – this attaches the PDF to the facade
        xmp.BindPdf(pdfStream);

        // ------------------------------------------------------------
        // 3. Retrieve the XMP metadata (as a byte array) and optionally save it
        // ------------------------------------------------------------
        byte[] xmpData = xmp.GetXmpMetadata();
        File.WriteAllBytes("output.xmp", xmpData);

        Console.WriteLine($"XMP metadata extracted – {xmpData.Length} bytes written to output.xmp");
    }
}
