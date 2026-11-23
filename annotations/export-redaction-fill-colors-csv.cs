using System;
using System.IO;
using System.Globalization;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvReportPath = "redaction_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare the CSV file (overwrite if it exists)
            using (StreamWriter writer = new StreamWriter(csvReportPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,AnnotationIndex,FillColorR,FillColorG,FillColorB");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];
                    int annotationIdx = 0;

                    // Iterate through all annotations on the page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Process only RedactionAnnotation instances
                        if (annot is RedactionAnnotation redaction)
                        {
                            annotationIdx++;

                            // FillColor is of type Aspose.Pdf.Color – convert to System.Drawing.Color to read components
                            Aspose.Pdf.Color pdfFillColor = redaction.FillColor;
                            System.Drawing.Color rgb = pdfFillColor != null ? pdfFillColor.ToRgb() : System.Drawing.Color.Empty;

                            double r = rgb.R; // 0‑255
                            double g = rgb.G;
                            double b = rgb.B;

                            // Write a CSV line for this annotation
                            writer.WriteLine(string.Format(CultureInfo.InvariantCulture,
                                "{0},{1},{2:F3},{3:F3},{4:F3}",
                                pageNum,
                                annotationIdx,
                                r,
                                g,
                                b));
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Redaction annotation report generated: {csvReportPath}");
    }
}
