using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // PDF to copy XMP metadata from
        const string targetPdfPath = "target.pdf";   // PDF that will receive the metadata
        const string outputPdfPath = "merged_output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        try
        {
            // ----- Extract XMP metadata from source PDF -----
            byte[] xmpBytes;
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(sourcePdfPath);
                xmpBytes = xmp.GetXmpMetadata(); // returns the whole XMP packet as byte[]
            }

            // ----- Load target PDF, apply XMP metadata, then merge pages -----
            using (Document targetDoc = new Document(targetPdfPath))
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Apply extracted XMP metadata to the target document
                using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
                {
                    targetDoc.SetXmpMetadata(xmpStream);
                }

                // Merge pages from source into target (pages are 1‑based, Add works with collections)
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Save the merged document with the transferred metadata
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}