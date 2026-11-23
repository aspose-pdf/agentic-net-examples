using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string outputPath = "page.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Root collection of bookmarks (OutlineCollection)
            var outlines = doc.Outlines;
            int pageNumber = -1;

            // Recursive search because bookmarks can be nested
            void FindBookmark(System.Collections.IEnumerable collection)
            {
                foreach (OutlineItemCollection item in collection)
                {
                    if (!string.IsNullOrEmpty(item.Title) &&
                        item.Title.Equals("Chapter One", StringComparison.OrdinalIgnoreCase))
                    {
                        // Direct destination (explicit destination types)
                        if (item.Destination != null)
                        {
                            // Most common explicit destination is XYZExplicitDestination
                            if (item.Destination is XYZExplicitDestination xyzDest)
                            {
                                pageNumber = xyzDest.PageNumber; // 1‑based indexing
                            }
                            else if (item.Destination is FitExplicitDestination fitDest)
                            {
                                pageNumber = fitDest.PageNumber;
                            }
                            // Add other explicit‑destination casts if needed
                        }
                        // Fallback for action‑based destinations
                        else if (item.Action != null && item.Action is GoToAction goTo && goTo.Destination != null)
                        {
                            if (goTo.Destination is XYZExplicitDestination xyzDest)
                                pageNumber = xyzDest.PageNumber;
                            else if (goTo.Destination is FitExplicitDestination fitDest)
                                pageNumber = fitDest.PageNumber;
                        }
                        return; // bookmark found – stop further search
                    }

                    // Recurse into child bookmarks (the item itself is a collection of its children)
                    if (item.Count > 0)
                    {
                        FindBookmark(item);
                        if (pageNumber != -1)
                            return;
                    }
                }
            }

            FindBookmark(outlines);

            if (pageNumber == -1)
            {
                Console.Error.WriteLine("Bookmark 'Chapter One' not found.");
                return;
            }

            // Save the page number to a plain text file
            File.WriteAllText(outputPath, pageNumber.ToString());
            Console.WriteLine($"Bookmark 'Chapter One' points to page {pageNumber}. Saved to {outputPath}.");
        }
    }
}
