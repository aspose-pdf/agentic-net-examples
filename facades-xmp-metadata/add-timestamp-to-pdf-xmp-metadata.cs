using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_timestamp.pdf";

        // Create a new PDF document and add a blank page.
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize the XMP metadata facade with the document.
            using (Aspose.Pdf.Facades.PdfXmpMetadata xmp = new Aspose.Pdf.Facades.PdfXmpMetadata(doc))
            {
                // Add a timestamp (ISO‑8601 format) to the ModifyDate property.
                string timestamp = DateTime.UtcNow.ToString("o");
                xmp.Add("xmp:ModifyDate", timestamp);

                // Save the PDF with the updated metadata.
                // On non‑Windows platforms Document.Save may require libgdiplus; guard the call.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    xmp.Save(outputPath);
                }
                else
                {
                    // Fallback: save via the Document object (metadata is already attached).
                    doc.Save(outputPath);
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with timestamp metadata.");
    }
}