using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Ensure the PDF file exists. If it does not, create a minimal PDF so the example can run.
        if (!File.Exists(inputPdf))
        {
            using (Document doc = new Document())
            {
                // Add a blank page.
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Detect XMP metadata using the Facades API.
        // PdfXmpMetadata implements IDisposable, so wrap it in a using block.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the PDF file to the facade.
            xmp.BindPdf(inputPdf);

            // Retrieve the whole XMP packet as a byte array.
            // If the PDF has no XMP metadata, the returned array will be empty.
            byte[] xmpData = xmp.GetXmpMetadata();

            bool hasXmp = xmpData != null && xmpData.Length > 0;

            Console.WriteLine($"XMP metadata present: {hasXmp}");
        }
    }
}
