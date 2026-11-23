using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string outputPath = "top_pages.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Collect the page numbers of top‑level bookmark items (outlines)
            List<string> lines = new List<string>();

            // Document.Outlines is the collection of top‑level bookmark items
            foreach (OutlineItemCollection bookmark in doc.Outlines)
            {
                int pageNumber = 0; // default placeholder when destination is missing or unsupported

                if (bookmark.Destination != null)
                {
                    // Destination implements IAppointment; cast to a concrete explicit‑destination type
                    if (bookmark.Destination is XYZExplicitDestination xyz)
                        pageNumber = xyz.PageNumber;
                    else if (bookmark.Destination is FitExplicitDestination fit)
                        pageNumber = fit.PageNumber;
                    else if (bookmark.Destination is FitHExplicitDestination fith)
                        pageNumber = fith.PageNumber;
                    else if (bookmark.Destination is FitVExplicitDestination fitv)
                        pageNumber = fitv.PageNumber;
                    else if (bookmark.Destination is FitRExplicitDestination fitr)
                        pageNumber = fitr.PageNumber;
                    // NOTE: Aspose.Pdf does not expose a Destination property on NamedDestination.
                    // Named destinations are normally accessed via GoToAction, not via the Destination
                    // property of a bookmark. Therefore we skip special handling for NamedDestination
                    // and keep the default placeholder (0) when such a case occurs.
                }

                lines.Add(pageNumber.ToString());
            }

            // Write the page numbers to the text file (one per line)
            File.WriteAllLines(outputPath, lines);
        }

        Console.WriteLine($"Top‑level bookmark page numbers saved to '{outputPath}'.");
    }
}
