using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationDeletionLogger
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_cleaned.pdf";
        const string logFile   = "deletion_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        // Initialize the annotation editor and bind the document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);

            // Prepare a list to hold annotation identifiers before deletion
            var annotationsToDelete = new List<(string Name, string Type, int PageNumber)>();

            // Iterate through all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];
                AnnotationCollection annColl = page.Annotations;

                // Collect information for each annotation on the page
                foreach (Annotation ann in annColl)
                {
                    string name = ann.Name ?? Guid.NewGuid().ToString(); // fallback if Name is null
                    string type = ann.AnnotationType.ToString();
                    annotationsToDelete.Add((name, type, pageIdx));
                }
            }

            // Write the log file
            using (StreamWriter logWriter = new StreamWriter(logFile, false))
            {
                logWriter.WriteLine("Name,Type,PageNumber"); // header
                foreach (var entry in annotationsToDelete)
                {
                    // Log the deletion action
                    logWriter.WriteLine($"{entry.Name},{entry.Type},{entry.PageNumber}");

                    // Delete the annotation by its name
                    editor.DeleteAnnotation(entry.Name);
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations deleted and logged to '{logFile}'. Clean PDF saved as '{outputPdf}'.");
    }
}