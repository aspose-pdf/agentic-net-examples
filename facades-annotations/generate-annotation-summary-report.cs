using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationReportGenerator
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs (first argument) or default "input_pdfs"
        string inputFolder = args.Length > 0 ? args[0] : "input_pdfs";
        // Output CSV report file (second argument) or default "annotation_report.csv"
        string reportPath = args.Length > 1 ? args[1] : "annotation_report.csv";

        // Verify that the input folder exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            // Still create an empty report with only the header so downstream tools have a file.
            File.WriteAllLines(reportPath, new List<string> { "File,AnnotationType,Count" });
            return;
        }

        // Gather all PDF files in the specified folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // Prepare CSV header
        var reportLines = new List<string> { "File,AnnotationType,Count" };

        foreach (string pdfPath in pdfFiles)
        {
            // Use PdfAnnotationEditor (Facades API) to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF file to the editor (load operation)
                editor.BindPdf(pdfPath);

                // Get total page count from the underlying Document
                int pageCount = editor.Document.Pages.Count;

                // Retrieve all possible annotation types
                AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

                // Extract annotations of all types from the whole document (pages are 1‑based)
                IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, allTypes);

                // Count occurrences per annotation type
                var typeCounts = new Dictionary<AnnotationType, int>();
                foreach (Annotation annot in annotations)
                {
                    AnnotationType type = annot.AnnotationType;
                    if (typeCounts.ContainsKey(type))
                        typeCounts[type]++;
                    else
                        typeCounts[type] = 1;
                }

                // Append counts to the CSV report
                foreach (KeyValuePair<AnnotationType, int> kvp in typeCounts)
                {
                    string line = $"{Path.GetFileName(pdfPath)},{kvp.Key},{kvp.Value}";
                    reportLines.Add(line);
                }
            }
        }

        // Write the CSV report to the specified file (even if no PDFs were found, the header is written)
        File.WriteAllLines(reportPath, reportLines);
        Console.WriteLine($"Annotation summary report saved to '{reportPath}'.");
    }
}
