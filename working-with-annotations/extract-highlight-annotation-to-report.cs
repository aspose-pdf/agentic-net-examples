using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";      // PDF containing highlight annotations
        const string reportPdfPath = "report.pdf";      // Output report PDF

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF and extract highlight annotation appearances
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Create a new PDF that will serve as the report
            using (Document reportDoc = new Document())
            {
                // Add a page to the report where annotations will be placed
                Page reportPage = reportDoc.Pages.Add();

                // Iterate through all pages and annotations
                for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
                {
                    Page srcPage = srcDoc.Pages[pageIndex];
                    foreach (Annotation annot in srcPage.Annotations)
                    {
                        // Process only highlight annotations
                        if (annot is HighlightAnnotation highlight)
                        {
                            // -----------------------------------------------------------------
                            // NOTE: HighlightAnnotation does NOT expose a GetAppearance() method.
                            // The correct way to obtain the visual representation is via the
                            // Appearance dictionary (key "N" for the normal appearance).  However,
                            // the Aspose.Pdf core API does not provide a direct way to convert that
                            // XForm into an image.  The most reliable approach is to copy the
                            // annotation itself onto the target page – the appearance will be
                            // rendered automatically by the PDF viewer.
                            // -----------------------------------------------------------------

                            // Create a new highlight annotation on the report page with the same
                            // rectangle and color as the source annotation.
                            HighlightAnnotation copiedHighlight = new HighlightAnnotation(reportPage, highlight.Rect)
                            {
                                Color = highlight.Color,
                                // Preserve any additional properties you might need (e.g., QuadPoints)
                                QuadPoints = highlight.QuadPoints
                            };

                            // Add the copied annotation to the report page.
                            reportPage.Annotations.Add(copiedHighlight);
                        }
                    }
                }

                // Save the report PDF
                reportDoc.Save(reportPdfPath);
                Console.WriteLine($"Report generated: {reportPdfPath}");
            }
        }
    }
}
