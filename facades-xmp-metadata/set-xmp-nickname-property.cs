using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";
        const string nickname = "CustomIdentifier";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF in memory (so we don't depend on an external file)
        // ---------------------------------------------------------------------
        using (var doc = new Document())
        {
            // Add a blank page – the PDF must contain at least one page for XMP
            doc.Pages.Add();

            // Save the PDF to a memory stream
            using (var inputStream = new MemoryStream())
            {
                doc.Save(inputStream);
                inputStream.Position = 0; // rewind for reading

                // ---------------------------------------------------------------
                // 2. Bind the XMP metadata facade to the in‑memory PDF stream
                // ---------------------------------------------------------------
                var xmp = new PdfXmpMetadata();
                xmp.BindPdf(inputStream);

                // ---------------------------------------------------------------
                // 3. Set (or replace) the xmp:Nickname property with the desired value
                // ---------------------------------------------------------------
                xmp.Add("xmp:Nickname", nickname);

                // ---------------------------------------------------------------
                // 4. Save the updated PDF to the output file
                // ---------------------------------------------------------------
                xmp.Save(outputPdf);
            }
        }
    }
}
