using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, XFDF intermediate file and verbose log file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string logFilePath   = "annotation_workflow.log";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open a StreamWriter for the verbose log (overwrite mode)
        using (StreamWriter logWriter = new StreamWriter(logFilePath, append: false))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPdfPath))
                {
                    logWriter.WriteLine($"{DateTime.Now:u} - Loaded PDF document from '{inputPdfPath}'.");

                    // Initialize PdfAnnotationEditor facade and bind the loaded document
                    using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
                    {
                        annotationEditor.BindPdf(doc);
                        logWriter.WriteLine($"{DateTime.Now:u} - Bound PdfAnnotationEditor to the document.");

                        // Export existing annotations to an XFDF file (stream overload)
                        using (FileStream exportStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                        {
                            annotationEditor.ExportAnnotationsToXfdf(exportStream);
                        }
                        logWriter.WriteLine($"{DateTime.Now:u} - Exported annotations to XFDF file '{xfdfPath}'.");

                        // (Optional) Here you could modify the XFDF file externally if needed

                        // Import annotations back from the XFDF file (stream overload)
                        using (FileStream importStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
                        {
                            annotationEditor.ImportAnnotationsFromXfdf(importStream);
                        }
                        logWriter.WriteLine($"{DateTime.Now:u} - Imported annotations from XFDF file '{xfdfPath}'.");

                        // Flatten all annotations into the page content
                        annotationEditor.FlatteningAnnotations();
                        logWriter.WriteLine($"{DateTime.Now:u} - Flattened all annotations.");

                        // Save the modified PDF document
                        doc.Save(outputPdfPath);
                        logWriter.WriteLine($"{DateTime.Now:u} - Saved modified PDF to '{outputPdfPath}'.");
                    } // PdfAnnotationEditor disposed here
                } // Document disposed here

                logWriter.WriteLine($"{DateTime.Now:u} - Annotation workflow completed successfully.");
            }
            catch (Exception ex)
            {
                // Log any exception details
                logWriter.WriteLine($"{DateTime.Now:u} - ERROR: {ex.GetType().Name} - {ex.Message}");
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
