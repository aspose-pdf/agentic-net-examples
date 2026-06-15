using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a tiny PNG image to be used as a stamp (1x1 pixel transparent)
        string imagePath = "stamp.png";
        byte[] pngBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=");
        File.WriteAllBytes(imagePath, pngBytes);

        // ---------------------------------------------------------------------
        // Step 1: Create a sample PDF with a single page and a bookmark (outline)
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add one blank page
            doc.Pages.Add();

            // Add a simple text fragment so the page is not empty
            TextFragment tf = new TextFragment("Sample PDF with bookmark");
            doc.Pages[1].Paragraphs.Add(tf);

            // Create a bookmark that points to the first page
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines);
            bookmark.Title = "First Page";
            bookmark.Action = new GoToAction(doc.Pages[1]);
            doc.Outlines.Add(bookmark);

            // Save the sample PDF
            doc.Save("input.pdf");
        }

        // ---------------------------------------------------------------
        // Step 2: Open the PDF and add an image stamp to each page
        // ---------------------------------------------------------------
        using (Document pdfDoc = new Document("input.pdf"))
        {
            int pageCount = pdfDoc.Pages.Count;
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                // Create an ImageStamp from the PNG file
                ImageStamp stamp = new ImageStamp(imagePath);
                // Position the stamp at the bottom‑center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Bottom;
                // Make the stamp semi‑transparent
                stamp.Opacity = 0.5f;
                // Add the stamp to the current page
                pdfDoc.Pages[pageIndex].AddStamp(stamp);
            }

            // Save the modified PDF – bookmarks are preserved automatically
            pdfDoc.Save("output.pdf");
        }
    }
}
