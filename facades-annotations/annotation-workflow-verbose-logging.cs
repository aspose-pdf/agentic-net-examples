using System;
using System.IO;
using System.Drawing; // for Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string xfdfFile   = "annotations.xfdf";
        const string logFile    = "annotation_workflow.log";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open a log writer (verbose logging)
        using (StreamWriter logWriter = new StreamWriter(logFile, false))
        {
            try
            {
                // -------------------------------------------------
                // STEP 1: Load the PDF document
                // -------------------------------------------------
                logWriter.WriteLine($"{DateTime.Now:u} - Loading PDF document: {inputPdf}");
                using (Document doc = new Document(inputPdf))
                {
                    // -------------------------------------------------
                    // STEP 2: Bind the document to PdfContentEditor (for creating annotations)
                    // -------------------------------------------------
                    logWriter.WriteLine($"{DateTime.Now:u} - Binding PDF to PdfContentEditor");
                    using (PdfContentEditor contentEditor = new PdfContentEditor())
                    {
                        contentEditor.BindPdf(doc);

                        // -------------------------------------------------
                        // STEP 3: Create a simple text annotation
                        // -------------------------------------------------
                        // System.Drawing.Rectangle expects (x, y, width, height)
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);
                        const string annotationText = "Diagnostic annotation";
                        const string author = "DiagnosticTool";
                        const bool isOpen = true;
                        const string subject = "WorkflowLog";
                        const int flags = 0; // default flags

                        logWriter.WriteLine($"{DateTime.Now:u} - Creating text annotation at {rect}");
                        contentEditor.CreateText(rect, annotationText, author, isOpen, subject, flags);
                    }

                    // -------------------------------------------------
                    // STEP 4: Export current annotations to XFDF
                    // -------------------------------------------------
                    logWriter.WriteLine($"{DateTime.Now:u} - Exporting annotations to XFDF: {xfdfFile}");
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    {
                        annotationEditor.BindPdf(doc);
                        using (FileStream xfdfOut = new FileStream(xfdfFile, FileMode.Create, FileAccess.Write))
                        {
                            annotationEditor.ExportAnnotationsToXfdf(xfdfOut);
                        }
                    }

                    // -------------------------------------------------
                    // STEP 5: Import annotations back from XFDF (demonstration)
                    // -------------------------------------------------
                    logWriter.WriteLine($"{DateTime.Now:u} - Importing annotations from XFDF: {xfdfFile}");
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    {
                        annotationEditor.BindPdf(doc);
                        using (FileStream xfdfIn = new FileStream(xfdfFile, FileMode.Open, FileAccess.Read))
                        {
                            annotationEditor.ImportAnnotationsFromXfdf(xfdfIn);
                        }
                    }

                    // -------------------------------------------------
                    // STEP 6: Flatten all annotations (make them part of page content)
                    // -------------------------------------------------
                    logWriter.WriteLine($"{DateTime.Now:u} - Flattening annotations");
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    {
                        annotationEditor.BindPdf(doc);
                        annotationEditor.FlatteningAnnotations();
                    }

                    // -------------------------------------------------
                    // STEP 7: Save the modified PDF document
                    // -------------------------------------------------
                    logWriter.WriteLine($"{DateTime.Now:u} - Saving modified PDF to: {outputPdf}");
                    doc.Save(outputPdf);
                }

                logWriter.WriteLine($"{DateTime.Now:u} - Annotation workflow completed successfully.");
            }
            catch (Exception ex)
            {
                logWriter.WriteLine($"{DateTime.Now:u} - ERROR: {ex.Message}");
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }

        Console.WriteLine($"Process finished. Log written to '{logFile}'.");
    }
}
