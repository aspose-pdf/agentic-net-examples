using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // <-- added namespace for Annotation types

class AnnotationSummaryUtility
{
    static void Main(string[] args)
    {
        // Expect one or more PDF file paths as command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: AnnotationSummaryUtility <pdf1> [<pdf2> ...]");
            return;
        }

        // Prepare a summary report file.
        string reportPath = "AnnotationSummaryReport.txt";
        using (StreamWriter reportWriter = new StreamWriter(reportPath, false))
        {
            foreach (string inputPath in args)
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    continue;
                }

                try
                {
                    // ---------- Lifecycle: create, load, save ----------
                    // Create the PdfAnnotationEditor facade.
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        // Load (bind) the PDF document.
                        editor.BindPdf(inputPath);

                        // Access the underlying Document to enumerate pages.
                        Document doc = editor.Document;

                        // Dictionary to hold annotation type name -> count.
                        Dictionary<string, int> typeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                        // Iterate all pages (Aspose.Pdf uses 1‑based indexing).
                        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                        {
                            Page page = doc.Pages[pageNum];
                            // Each page has an Annotations collection.
                            foreach (Annotation annot in page.Annotations)
                            {
                                string typeName = annot.GetType().Name; // e.g., TextAnnotation, HighlightAnnotation, etc.
                                if (typeCounts.ContainsKey(typeName))
                                    typeCounts[typeName]++;
                                else
                                    typeCounts[typeName] = 1;
                            }
                        }

                        // Write per‑file summary to the report.
                        reportWriter.WriteLine($"File: {Path.GetFileName(inputPath)}");
                        if (typeCounts.Count == 0)
                        {
                            reportWriter.WriteLine("  No annotations found.");
                        }
                        else
                        {
                            foreach (var kvp in typeCounts)
                            {
                                reportWriter.WriteLine($"  {kvp.Key}: {kvp.Value}");
                            }
                        }
                        reportWriter.WriteLine(); // blank line between files

                        // Optional: save a copy of the PDF (unchanged) to satisfy the save rule.
                        string outputCopyPath = Path.Combine(
                            Path.GetDirectoryName(inputPath) ?? "",
                            Path.GetFileNameWithoutExtension(inputPath) + "_processed.pdf");
                        editor.Save(outputCopyPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Annotation summary written to '{reportPath}'.");
    }
}
