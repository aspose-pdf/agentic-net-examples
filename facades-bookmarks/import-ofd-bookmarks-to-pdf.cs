using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";      // Existing PDF to receive bookmarks
        const string ofdPath       = "bookmarks.ofd"; // OFD file containing the bookmark hierarchy
        const string outputPdfPath = "output.pdf";     // Resulting PDF with imported bookmarks

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD file not found: {ofdPath}");
            return;
        }

        // Load the target PDF (the document that will receive the bookmarks)
        using (Document targetPdf = new Document(sourcePdfPath))
        {
            // Load the OFD file. Aspose.Pdf auto‑detects the format when no explicit load options are supplied.
            Document ofdDoc = new Document(ofdPath);

            // Helper to obtain a page number from an outline entry. If the entry has a concrete
            // explicit destination we extract its page number; otherwise we fall back to page 1.
            int GetPageNumber(OutlineItemCollection item)
            {
                if (item.Destination is ExplicitDestination explicitDest && explicitDest.PageNumber > 0)
                    return explicitDest.PageNumber;
                return 1;
            }

            // Recursively convert an OFD outline entry into a PDF outline entry.
            OutlineItemCollection ConvertOutline(OutlineItemCollection ofdItem)
            {
                // Create a new outline item for the target PDF.
                var pdfItem = new OutlineItemCollection(targetPdf.Outlines)
                {
                    Title = ofdItem.Title,
                    // Use a GoToAction that points to the appropriate page in the target PDF.
                    Destination = new GoToAction(targetPdf.Pages[GetPageNumber(ofdItem)])
                };

                // Process child outlines recursively.
                foreach (OutlineItemCollection child in ofdItem)
                {
                    pdfItem.Add(ConvertOutline(child));
                }

                return pdfItem;
            }

            // Iterate over the root outline collection of the OFD document and add each converted
            // outline to the target PDF's root outline collection.
            foreach (OutlineItemCollection rootItem in ofdDoc.Outlines)
            {
                targetPdf.Outlines.Add(ConvertOutline(rootItem));
            }

            // Save the updated PDF.
            targetPdf.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks imported from OFD and saved to '{outputPdfPath}'.");
    }
}
