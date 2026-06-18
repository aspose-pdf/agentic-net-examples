using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationSummaryUtility
{
    static void Main(string[] args)
    {
        // Expect one or more PDF file paths as command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: AnnotationSummaryUtility <pdfPath1> [<pdfPath2> ...]");
            return;
        }

        // Prepare a simple text report.
        string reportPath = "AnnotationSummaryReport.txt";
        using (StreamWriter reportWriter = new StreamWriter(reportPath, false))
        {
            foreach (string pdfPath in args)
            {
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    reportWriter.WriteLine($"{pdfPath}: File not found");
                    continue;
                }

                try
                {
                    // Create the facade and bind the PDF (load).
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(pdfPath);

                        // Get total number of pages (1‑based indexing).
                        int pageCount = editor.Document.Pages.Count;

                        // Retrieve all possible annotation types.
                        AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

                        // Extract all annotations in the document.
                        IList<Aspose.Pdf.Annotations.Annotation> annotations =
                            editor.ExtractAnnotations(1, pageCount, allTypes);

                        // Count annotations per type.
                        Dictionary<AnnotationType, int> typeCounts = new Dictionary<AnnotationType, int>();
                        foreach (Aspose.Pdf.Annotations.Annotation annot in annotations)
                        {
                            AnnotationType type = annot.AnnotationType;
                            if (typeCounts.ContainsKey(type))
                                typeCounts[type]++;
                            else
                                typeCounts[type] = 1;
                        }

                        // Write summary for this PDF.
                        Console.WriteLine($"Processed: {pdfPath}");
                        reportWriter.WriteLine($"File: {pdfPath}");
                        if (typeCounts.Count == 0)
                        {
                            Console.WriteLine("  No annotations found.");
                            reportWriter.WriteLine("  No annotations found.");
                        }
                        else
                        {
                            foreach (KeyValuePair<AnnotationType, int> kvp in typeCounts)
                            {
                                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                                reportWriter.WriteLine($"  {kvp.Key}: {kvp.Value}");
                            }
                        }
                        reportWriter.WriteLine(); // blank line between files
                    } // editor disposed here (load resources released)
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
                    reportWriter.WriteLine($"{pdfPath}: Error - {ex.Message}");
                    reportWriter.WriteLine();
                }
            }
        }

        Console.WriteLine($"Annotation summary written to '{reportPath}'.");
    }
}