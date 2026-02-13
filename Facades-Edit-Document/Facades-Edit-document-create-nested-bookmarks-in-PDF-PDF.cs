using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Determine safe page numbers (Aspose.Pdf pages are 1‑based)
            int firstPage = pdfDocument.Pages.Count >= 1 ? 1 : 0;
            int secondPage = pdfDocument.Pages.Count >= 2 ? 2 : firstPage;

            if (firstPage == 0)
            {
                Console.Error.WriteLine("Error: The PDF does not contain any pages.");
                return;
            }

            // Create a bookmark editor and bind it to the loaded document
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(pdfDocument);

                // ----- Create Parent Bookmark -----
                OutlineItemCollection parentBookmark = new OutlineItemCollection(pdfDocument.Outlines)
                {
                    Title = "Parent Bookmark",
                    Action = new GoToAction(new XYZExplicitDestination(
                        pdfDocument.Pages[firstPage],
                        0,
                        pdfDocument.Pages[firstPage].PageInfo.Height,
                        1))
                };

                // ----- Create Child Bookmark -----
                OutlineItemCollection childBookmark = new OutlineItemCollection(pdfDocument.Outlines)
                {
                    Title = "Child Bookmark",
                    Action = new GoToAction(new XYZExplicitDestination(
                        pdfDocument.Pages[secondPage],
                        0,
                        pdfDocument.Pages[secondPage].PageInfo.Height,
                        1))
                };

                // Nest the child under the parent
                parentBookmark.Add(childBookmark);

                // Add the parent (which now contains the child) to the document's outline collection
                pdfDocument.Outlines.Add(parentBookmark);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Nested bookmarks created and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
