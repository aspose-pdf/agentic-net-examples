using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvReportPath = "caret_annotations_report.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare CSV writer
                using (StreamWriter csvWriter = new StreamWriter(csvReportPath, false))
                {
                    // CSV header
                    csvWriter.WriteLine("PageNumber,AnnotationIndex,Flags");

                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                    {
                        Page page = pdfDoc.Pages[pageNum];
                        int annotIndex = 0;

                        // Iterate all annotations on the page
                        foreach (Annotation annotation in page.Annotations)
                        {
                            // Filter only CaretAnnotations
                            if (annotation is CaretAnnotation caretAnnot)
                            {
                                // Flags is a bitmask; output its integer value
                                int flagsValue = (int)caretAnnot.Flags;
                                csvWriter.WriteLine($"{pageNum},{annotIndex},{flagsValue}");
                            }
                            annotIndex++;
                        }
                    }
                }
            }

            Console.WriteLine($"Caret annotation report generated at '{csvReportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}