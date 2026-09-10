using System;
using System.IO;
using System.Collections.Generic;
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

        // Collect annotation details before deletion
        var deletions = new List<(string Name, string Type, int Page)>();

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPdf);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annColl = page.Annotations;

                // Iterate through annotations on the current page
                for (int annIndex = 1; annIndex <= annColl.Count; annIndex++)
                {
                    Annotation ann = annColl[annIndex];
                    string name = ann.Name ?? string.Empty;
                    string type = ann.AnnotationType.ToString();
                    deletions.Add((name, type, pageIndex));
                }
            }

            // Write the log file (CSV: Name,Type,Page)
            using (StreamWriter logWriter = new StreamWriter(logFile, false))
            {
                logWriter.WriteLine("Name,Type,Page");
                foreach (var entry in deletions)
                {
                    logWriter.WriteLine($"{entry.Name},{entry.Type},{entry.Page}");
                }
            }

            // Delete each annotation by its name
            foreach (var entry in deletions)
            {
                // Skip empty names (some annotations may not have a name)
                if (!string.IsNullOrEmpty(entry.Name))
                {
                    editor.DeleteAnnotation(entry.Name);
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations deleted and logged to '{logFile}'.");
        Console.WriteLine($"Modified PDF saved as '{outputPdf}'.");
    }
}