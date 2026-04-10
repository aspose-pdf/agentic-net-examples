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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        // Initialize the annotation editor and bind the document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        // Open the log file for writing
        using (StreamWriter logWriter = new StreamWriter(logFile, false))
        {
            editor.BindPdf(doc);

            // Write CSV header
            logWriter.WriteLine("AnnotationName,AnnotationType,PageNumber");

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through each annotation on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    string name = annotation.Name ?? "Unnamed";
                    string type = annotation.AnnotationType.ToString();

                    // Log the deletion details
                    logWriter.WriteLine($"{name},{type},{pageIndex}");

                    // Delete the annotation by its name
                    editor.DeleteAnnotation(name);
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations deleted and logged to '{logFile}'. Output saved to '{outputPdf}'.");
    }
}