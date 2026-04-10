using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "merged_output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the source PDFs exist – create simple placeholder files if they
        // are missing. This removes the FileNotFoundException that caused the
        // original crash.
        // ---------------------------------------------------------------------
        if (!File.Exists(firstPdfPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("This is the first PDF."));
                doc.Save(firstPdfPath);
            }
        }

        if (!File.Exists(secondPdfPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("This is the second PDF."));
                doc.Save(secondPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // Load the first PDF, add a text annotation, and keep it in memory.
        // ---------------------------------------------------------------------
        using (Document firstDoc = new Document(firstPdfPath))
        {
            Page page = firstDoc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Added annotation via Aspose.Pdf",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // -----------------------------------------------------------------
            // Load the second PDF.
            // -----------------------------------------------------------------
            using (Document secondDoc = new Document(secondPdfPath))
            {
                // -----------------------------------------------------------------
                // Merge the two documents. The Document.Merge overload that accepts
                // Document instances preserves bookmarks and avoids the need for a
                // temporary file on disk.
                // -----------------------------------------------------------------
                using (Document merged = new Document())
                {
                    merged.Merge(firstDoc, secondDoc);
                    merged.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}
