using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction and related action types

class SplitPdfByChapter
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath      = "input.xml";          // Source XML
        const string outputFolder = "Chapters";           // Folder for split PDFs

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load XML and convert it to a PDF document
        // NOTE: XmlLoadOptions is required for XML → PDF conversion
        using (Document sourceDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // If the document has no outlines (bookmarks), treat the whole document as a single chapter
            if (sourceDoc.Outlines == null || sourceDoc.Outlines.Count == 0)
            {
                string singlePath = Path.Combine(outputFolder, "Chapter_1.pdf");
                sourceDoc.Save(singlePath);
                Console.WriteLine($"Saved single chapter PDF: {singlePath}");
                return;
            }

            // Gather start page numbers for each top‑level outline (assumed to represent chapters)
            int[] startPages = new int[sourceDoc.Outlines.Count];
            string[] titles   = new string[sourceDoc.Outlines.Count];

            for (int i = 0; i < sourceDoc.Outlines.Count; i++)
            {
                // Each entry in Outlines is an OutlineItemCollection (the singular outline object)
                OutlineItemCollection outline = sourceDoc.Outlines[i];
                titles[i] = SanitizeFileName(outline.Title ?? $"Chapter_{i + 1}");

                // The outline action should be a GoToAction pointing to a page
                if (outline.Action is GoToAction goTo && goTo.Destination is ExplicitDestination dest && dest.Page != null)
                {
                    startPages[i] = dest.Page.Number; // 1‑based page index
                }
                else
                {
                    // Fallback: if no explicit destination, use the current page index (i+1)
                    startPages[i] = i + 1;
                }
            }

            // Split the source PDF into separate chapter PDFs
            for (int i = 0; i < startPages.Length; i++)
            {
                int startPage = startPages[i];
                int endPage   = (i + 1 < startPages.Length) ? startPages[i + 1] - 1 : sourceDoc.Pages.Count;

                // Create a new document for the current chapter
                using (Document chapterDoc = new Document())
                {
                    // Add the required page range (Pages collection is 1‑based)
                    chapterDoc.Pages.Add(sourceDoc.Pages[startPage]);

                    for (int p = startPage + 1; p <= endPage; p++)
                    {
                        chapterDoc.Pages.Add(sourceDoc.Pages[p]);
                    }

                    // Build output file name
                    string chapterFile = Path.Combine(outputFolder, $"{titles[i]}.pdf");

                    // Save the chapter PDF
                    chapterDoc.Save(chapterFile);
                    Console.WriteLine($"Saved chapter PDF: {chapterFile}");
                }
            }
        }
    }

    // Helper: removes invalid filename characters
    private static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}
