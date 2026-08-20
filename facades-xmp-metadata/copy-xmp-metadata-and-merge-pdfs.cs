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
        const string outputPdfPath = "merged.pdf";   // Resulting PDF after merging pages

        // Ensure source and target files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Load source PDF and extract its XMP metadata using the Facade class
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            PdfXmpMetadata xmpFacade = new PdfXmpMetadata();
            xmpFacade.BindPdf(sourceDoc);
            byte[] xmpBytes = xmpFacade.GetXmpMetadata(); // XMP as XML bytes

            // Load target PDF, apply the extracted XMP metadata, then merge pages
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Apply XMP metadata to the target document
                using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
                {
                    targetDoc.SetXmpMetadata(xmpStream);
                }

                // Append all pages from the source PDF to the target PDF
                // Aspose.Pdf uses 1‑based page indexing; adding the whole collection is safe
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Save the merged document with the new metadata
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}