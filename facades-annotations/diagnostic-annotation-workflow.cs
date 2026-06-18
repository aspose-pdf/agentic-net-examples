using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationDiagnostic
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated_output.pdf";
        const string logPath   = "annotation_workflow.log";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open a log file for verbose output
        using (var logWriter = new StreamWriter(logPath, false))
        {
            try
            {
                logWriter.WriteLine($"[{DateTime.Now}] Starting annotation workflow.");

                // -------------------------------------------------
                // 1. Create PdfAnnotationEditor instance (creation)
                // -------------------------------------------------
                using (var editor = new PdfAnnotationEditor())
                {
                    logWriter.WriteLine($"[{DateTime.Now}] PdfAnnotationEditor instance created.");

                    // -------------------------------------------------
                    // 2. Bind the PDF document (loading)
                    // -------------------------------------------------
                    editor.BindPdf(inputPdf);
                    logWriter.WriteLine($"[{DateTime.Now}] PDF bound to editor: {inputPdf}");

                    // -------------------------------------------------
                    // 3. Add a TextAnnotation to the first page
                    // -------------------------------------------------
                    Page firstPage = editor.Document.Pages[1]; // 1‑based indexing
                    var textRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);
                    var textAnnotation = new TextAnnotation(firstPage, textRect)
                    {
                        Contents = "Diagnostic Text Annotation",
                        Color    = Aspose.Pdf.Color.Yellow,
                        // Make the annotation printable but not visible (optional)
                        Flags    = AnnotationFlags.Print | AnnotationFlags.NoView
                    };
                    firstPage.Annotations.Add(textAnnotation);
                    logWriter.WriteLine($"[{DateTime.Now}] TextAnnotation added (Page 1, Rect {textRect}).");

                    // -------------------------------------------------
                    // 4. Add a LinkAnnotation to the same page
                    // -------------------------------------------------
                    var linkRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
                    var linkAnnotation = new LinkAnnotation(firstPage, linkRect)
                    {
                        Action = new GoToURIAction("https://www.example.com"),
                        Color  = Aspose.Pdf.Color.Blue,
                        Contents = "Visit Example.com"
                    };
                    firstPage.Annotations.Add(linkAnnotation);
                    logWriter.WriteLine($"[{DateTime.Now}] LinkAnnotation added (Page 1, Rect {linkRect}).");

                    // -------------------------------------------------
                    // 5. Flatten all annotations (processing step)
                    // -------------------------------------------------
                    editor.FlatteningAnnotations();
                    logWriter.WriteLine($"[{DateTime.Now}] All annotations flattened.");

                    // -------------------------------------------------
                    // 6. Save the modified PDF (saving)
                    // -------------------------------------------------
                    editor.Save(outputPdf);
                    logWriter.WriteLine($"[{DateTime.Now}] Modified PDF saved to: {outputPdf}");
                }

                logWriter.WriteLine($"[{DateTime.Now}] Annotation workflow completed successfully.");
            }
            catch (Exception ex)
            {
                logWriter.WriteLine($"[{DateTime.Now}] ERROR: {ex.Message}");
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}