using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationReportUtility
{
    static void Main(string[] args)
    {
        // Determine the folder containing PDFs.
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Get all PDF files in the folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        var reportLines = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            // Use PdfAnnotationEditor to work with annotations.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document.
                editor.BindPdf(pdfPath);

                // Total pages in the document (1‑based indexing).
                int pageCount = editor.Document.Pages.Count;

                // Retrieve all possible annotation types.
                AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

                // Extract annotations of all types from the whole document.
                IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, allTypes);

                // Count annotations by their type.
                var typeCounts = new Dictionary<AnnotationType, int>();
                foreach (Annotation ann in annotations)
                {
                    AnnotationType type = ann.AnnotationType;
                    if (typeCounts.ContainsKey(type))
                        typeCounts[type]++;
                    else
                        typeCounts[type] = 1;
                }

                // Build report entries for this PDF.
                reportLines.Add($"File: {Path.GetFileName(pdfPath)}");
                if (typeCounts.Count == 0)
                {
                    reportLines.Add("  No annotations found.");
                }
                else
                {
                    foreach (var kvp in typeCounts.OrderBy(k => k.Key.ToString()))
                    {
                        reportLines.Add($"  {kvp.Key}: {kvp.Value}");
                    }
                }
                reportLines.Add(string.Empty);
            }
        }

        // Write the summary report to a text file in the same folder.
        string reportPath = Path.Combine(inputFolder, "AnnotationReport.txt");
        File.WriteAllLines(reportPath, reportLines);

        Console.WriteLine($"Annotation summary saved to '{reportPath}'.");
    }
}