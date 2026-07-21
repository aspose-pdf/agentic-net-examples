using System;
using System.IO;
using Aspose.Pdf; // Document
using Aspose.Pdf.Facades; // PdfAnnotationEditor
using Aspose.Pdf.Annotations; // Annotation types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "annotation_workflow.log";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        // Initialize the annotation editor bound to the document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
        // Open a StreamWriter for verbose logging
        using (StreamWriter log = new StreamWriter(logPath, false))
        {
            log.WriteLine($"[{DateTime.Now}] Opened document: {inputPath}");
            log.WriteLine($"[{DateTime.Now}] Page count: {doc.Pages.Count}");

            // Iterate through each page (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageIndex];
                log.WriteLine($"[{DateTime.Now}] Processing Page {pageIndex}");

                // Retrieve the annotation collection for the current page
                AnnotationCollection annotations = page.Annotations;
                log.WriteLine($"[{DateTime.Now}] Annotation count on page {pageIndex}: {annotations.Count}");

                // Iterate through annotations (1‑based indexing)
                for (int i = 1; i <= annotations.Count; i++)
                {
                    Annotation annotation = annotations[i];
                    string annotationType = annotation.AnnotationType.ToString();
                    string annotationName = annotation.Name ?? "(no name)";

                    log.WriteLine($"[{DateTime.Now}] Annotation {i} on page {pageIndex}: Type={annotationType}, Name={annotationName}");

                    // Example of handling a specific annotation type
                    if (annotation is LinkAnnotation link)
                    {
                        // Log link destination details
                        if (link.Destination != null)
                        {
                            log.WriteLine($"[{DateTime.Now}]   Link Destination: {link.Destination}");
                        }
                        else if (link.Action != null)
                        {
                            log.WriteLine($"[{DateTime.Now}]   Link Action: {link.Action.GetType().Name}");
                        }
                    }
                    else if (annotation is TextAnnotation textAnn)
                    {
                        // Log text annotation contents
                        log.WriteLine($"[{DateTime.Now}]   Text Contents: {textAnn.Contents}");
                    }
                    // Additional annotation types can be handled similarly
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
            log.WriteLine($"[{DateTime.Now}] Saved modified document to: {outputPath}");
        }

        Console.WriteLine($"Annotation workflow completed. Log written to '{logPath}'.");
    }
}