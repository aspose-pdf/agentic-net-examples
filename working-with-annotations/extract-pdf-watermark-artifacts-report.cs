using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class WatermarkExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPath = "watermark_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare CSV report header
            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine("PageNumber,X1,Y1,X2,Y2,Opacity");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Iterate all annotations on the page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Identify watermark annotations
                        if (annot is WatermarkAnnotation watermark)
                        {
                            // Position is defined by the rectangle (lower‑left and upper‑right corners)
                            // The correct property for the bounds of a WatermarkAnnotation is 'Rect'
                            Aspose.Pdf.Rectangle rect = watermark.Rect;

                            // Opacity is a value between 0 (transparent) and 1 (opaque)
                            double opacity = watermark.Opacity;

                            // Write details to CSV
                            writer.WriteLine($"{pageIndex},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY},{opacity}");
                        }
                    }
                }
            }

            Console.WriteLine($"Watermark report generated: {reportPath}");
        }
    }
}
