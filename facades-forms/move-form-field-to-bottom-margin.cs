using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // required for Field and specific form‑field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Bottom margin and side margins (in points; 1 inch = 72 points)
        const float bottomMargin = 20f;   // distance from the bottom edge
        const float leftMargin   = 50f;   // distance from the left edge
        const float rightMargin  = 50f;   // distance from the right edge
        const float fieldHeight  = 20f;   // approximate height of the field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Field
            Field? footerField = doc.Form["FooterNote"] as Field;
            if (footerField == null)
            {
                Console.Error.WriteLine("Field \"FooterNote\" not found in the document.");
                return;
            }

            // Re‑position the field on every page
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                double pageWidth = page.PageInfo.Width; // Width is double

                // Calculate rectangle for the new location (bottom margin)
                float left   = leftMargin;
                float bottom = bottomMargin;
                float right  = (float)(pageWidth - rightMargin);
                float top    = bottom + fieldHeight;

                // Rectangle constructor accepts double; float values are widened automatically
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(left, bottom, right, top);

                // Update the field appearance on the current page
                doc.Form.AddFieldAppearance(footerField, pageNum, rect);
            }

            // Use the Facades API (PdfFileStamp) to write the modified document
            using (PdfFileStamp stamp = new PdfFileStamp())
            {
                stamp.BindPdf(doc);
                stamp.Save(outputPath);
            }

            Console.WriteLine($"Field \"FooterNote\" moved to bottom margin and saved to '{outputPath}'.");
        }
    }
}
