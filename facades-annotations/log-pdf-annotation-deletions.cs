using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logFile   = "annotation_deletions_log.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        // Initialize the annotation editor and bind it to the opened document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);

            // Prepare the log file (CSV with header)
            using (StreamWriter logWriter = new StreamWriter(logFile, false))
            {
                logWriter.WriteLine("Name,Type,PageNumber");

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    // Iterate over each annotation on the current page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        string name = annotation.Name ?? string.Empty;
                        string type = annotation.AnnotationType.ToString();
                        // Write a log entry for this annotation
                        logWriter.WriteLine($"{name},{type},{pageNum}");
                    }
                }
            }

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations deleted and logged to '{logFile}'.");
        Console.WriteLine($"Modified PDF saved as '{outputPdf}'.");
    }
}