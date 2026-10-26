using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string reportPath = "watermark_report.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create or overwrite the CSV report file
                using (StreamWriter writer = new StreamWriter(reportPath, false))
                {
                    // CSV header
                    writer.WriteLine("Page,Left,Bottom,Right,Top,Opacity");

                    // Pages are 1‑based in Aspose.Pdf
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        // Iterate all annotations on the page
                        foreach (Annotation annot in page.Annotations)
                        {
                            // Watermarks are represented by WatermarkAnnotation
                            if (annot is WatermarkAnnotation watermark)
                            {
                                // Position rectangle (lower‑left X/Y, upper‑right X/Y)
                                Aspose.Pdf.Rectangle rect = watermark.Rect;

                                // Opacity value (0.0 = fully transparent, 1.0 = fully opaque)
                                double opacity = watermark.Opacity;

                                // Write a line to the CSV report
                                writer.WriteLine($"{i},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY},{opacity}");
                            }
                        }
                    }
                }

                Console.WriteLine($"Watermark report generated: {reportPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}