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
        const string logFile   = "deletion_log.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Access the underlying Document.
        Document doc = editor.Document;

        // Open a writer for the structured log (CSV format).
        using (StreamWriter logWriter = new StreamWriter(logFile, false))
        {
            // Write header line.
            logWriter.WriteLine("Name,Type,PageNumber");

            // Iterate through pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Delete annotations in reverse order to keep indexes valid.
                for (int annIndex = annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation ann = annotations[annIndex];

                    // Gather required information.
                    string name = ann.Name ?? string.Empty;
                    string type = ann.AnnotationType.ToString();
                    int    pageNumber = pageIndex;

                    // Log the deletion action.
                    logWriter.WriteLine($"{name},{type},{pageNumber}");

                    // Remove the annotation from the page.
                    annotations.Delete(annIndex);
                }
            }
        }

        // Save the modified PDF.
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Annotations deleted and logged to '{logFile}'.");
        Console.WriteLine($"Modified PDF saved as '{outputPdf}'.");
    }
}