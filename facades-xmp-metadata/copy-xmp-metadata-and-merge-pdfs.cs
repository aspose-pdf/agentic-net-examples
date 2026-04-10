using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // PDF containing the XMP metadata
        const string targetPdfPath = "target.pdf";   // PDF that will receive the metadata
        const string outputPdfPath = "merged.pdf";   // Resulting PDF after pages are merged

        // Verify that input files exist
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

        // Extract XMP metadata from the source PDF
        byte[] xmpData;
        using (var xmpFacade = new PdfXmpMetadata())
        {
            xmpFacade.BindPdf(sourcePdfPath);
            xmpData = xmpFacade.GetXmpMetadata();
        }

        // Load the target PDF, apply the extracted XMP metadata, then merge pages
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Apply XMP metadata to the target document
            using (MemoryStream xmpStream = new MemoryStream(xmpData))
            {
                targetDoc.SetXmpMetadata(xmpStream);
            }

            // Load the source PDF (pages to be merged) – separate instance to avoid disposing the earlier one
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Append all pages from the source PDF to the target PDF
                targetDoc.Pages.Add(sourceDoc.Pages);
            }

            // Save the merged document with the transferred XMP metadata
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}