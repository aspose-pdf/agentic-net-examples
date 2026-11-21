using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationDiagnostic
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string tempPath       = "temp_with_annotation.pdf";
        const string outputPath     = "output.pdf";
        const string logFilePath    = "annotation_workflow.log";

        // Ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open log file for verbose output
        using (StreamWriter log = new StreamWriter(logFilePath, false))
        {
            try
            {
                log.WriteLine($"{DateTime.Now:u} - Starting annotation workflow.");

                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    log.WriteLine($"{DateTime.Now:u} - Loaded PDF. Page count: {doc.Pages.Count}");

                    // Create a simple text annotation on the first page
                    Page firstPage = doc.Pages[1]; // 1‑based indexing
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                    TextAnnotation txtAnn = new TextAnnotation(firstPage, rect)
                    {
                        Title    = "Diagnostic",
                        Contents = "Annotation added in diagnostic mode.",
                        Open     = true,
                        Icon     = TextIcon.Note
                    };
                    firstPage.Annotations.Add(txtAnn);
                    log.WriteLine($"{DateTime.Now:u} - Added TextAnnotation to page 1.");

                    // Save the modified PDF to a temporary file for further processing
                    doc.Save(tempPath);
                    log.WriteLine($"{DateTime.Now:u} - Saved intermediate PDF with annotation to '{tempPath}'.");
                }

                // Use PdfAnnotationEditor to manipulate annotations (e.g., delete, flatten)
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(tempPath);
                    log.WriteLine($"{DateTime.Now:u} - PdfAnnotationEditor bound to '{tempPath}'.");

                    // Example operation: delete existing annotations (if any)
                    editor.DeleteAnnotations();
                    log.WriteLine($"{DateTime.Now:u} - Deleted all annotations via editor.");

                    // Flatten remaining annotations into the page content
                    editor.FlatteningAnnotations();
                    log.WriteLine($"{DateTime.Now:u} - Flattened annotations.");

                    // Save the final PDF
                    editor.Save(outputPath);
                    log.WriteLine($"{DateTime.Now:u} - Saved final PDF to '{outputPath}'.");
                }

                log.WriteLine($"{DateTime.Now:u} - Annotation workflow completed successfully.");
            }
            catch (Exception ex)
            {
                log.WriteLine($"{DateTime.Now:u} - ERROR: {ex.Message}");
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}