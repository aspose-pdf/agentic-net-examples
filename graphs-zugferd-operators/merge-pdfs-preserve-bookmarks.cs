using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace MergePdfsPreserveBookmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create first sample PDF with a bookmark
            using (Document doc1 = new Document())
            {
                // Add a single page
                Page page1 = doc1.Pages.Add();

                // Create a bookmark that points to the first page
                OutlineItemCollection bookmark1 = new OutlineItemCollection(doc1.Outlines);
                bookmark1.Title = "Document 1 - Page 1";
                bookmark1.Action = new GoToAction(page1);
                doc1.Outlines.Add(bookmark1);

                // Save the first PDF
                doc1.Save("doc1.pdf");
            }

            // Create second sample PDF with a bookmark
            using (Document doc2 = new Document())
            {
                // Add a single page
                Page page2 = doc2.Pages.Add();

                // Create a bookmark that points to the first page of the second document
                OutlineItemCollection bookmark2 = new OutlineItemCollection(doc2.Outlines);
                bookmark2.Title = "Document 2 - Page 1";
                bookmark2.Action = new GoToAction(page2);
                doc2.Outlines.Add(bookmark2);

                // Save the second PDF
                doc2.Save("doc2.pdf");
            }

            // Merge the two PDFs while preserving their bookmark hierarchy
            Document mergedDocument = Document.MergeDocuments("doc1.pdf", "doc2.pdf");
            using (mergedDocument)
            {
                // Optionally set the page mode so the outline panel is shown when opened
                mergedDocument.PageMode = PageMode.UseOutlines;

                // Save the merged PDF
                mergedDocument.Save("merged.pdf");
            }
        }
    }
}
