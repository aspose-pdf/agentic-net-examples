using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and log file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logFilePath = "annotation_log.txt";

        // Open a StreamWriter for the verbose log
        using (StreamWriter logWriter = new StreamWriter(logFilePath, false))
        {
            void Log(string message) => logWriter.WriteLine($"{DateTime.Now:O} - {message}");

            Log("=== Annotation workflow started ===");

            // Ensure the input PDF exists – create a minimal one if it does not.
            if (!File.Exists(inputPdfPath))
            {
                Log($"Input file '{inputPdfPath}' not found. Creating a placeholder PDF.");
                CreatePlaceholderPdf(inputPdfPath, Log);
                Log($"Placeholder PDF created at '{inputPdfPath}'.");
            }

            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPdfPath))
            {
                Log($"Document loaded: '{inputPdfPath}'. Pages = {doc.Pages.Count}");

                // Create the PdfAnnotationEditor facade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the loaded document to the editor
                    editor.BindPdf(doc);
                    Log("PdfAnnotationEditor bound to document.");

                    // Export current annotations to a temporary XFDF file (for diagnostic purposes)
                    string tempXfdfPath = Path.GetTempFileName();
                    using (FileStream xfdfStream = new FileStream(tempXfdfPath, FileMode.Create, FileAccess.Write))
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);
                    }
                    Log($"Exported existing annotations to temporary XFDF: '{tempXfdfPath}'.");

                    // Count total annotations in the document
                    int totalAnnotations = 0;
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        totalAnnotations += doc.Pages[i].Annotations.Count;
                    }
                    Log($"Total annotations before modification: {totalAnnotations}");

                    // Example operation: delete all Text annotations
                    editor.DeleteAnnotations("Text");
                    Log("Deleted all Text annotations.");

                    // Re‑count annotations after deletion
                    int remainingAnnotations = 0;
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        remainingAnnotations += doc.Pages[i].Annotations.Count;
                    }
                    Log($"Annotations remaining after deletion: {remainingAnnotations}");

                    // Save the modified document using the facade (lifecycle rule: use Save)
                    editor.Save(outputPdfPath);
                    Log($"Modified document saved to: '{outputPdfPath}'");
                }

                Log("PdfAnnotationEditor disposed.");
            }

            Log("Document disposed.");
            Log("=== Annotation workflow completed ===");
        }
    }

    /// <summary>
    /// Creates a minimal placeholder PDF containing a single blank page.
    /// </summary>
    /// <param name="path">File path where the PDF will be saved.</param>
    /// <param name="logAction">Delegate used to write diagnostic messages.</param>
    private static void CreatePlaceholderPdf(string path, Action<string> logAction)
    {
        try
        {
            using (Document placeholder = new Document())
            {
                placeholder.Pages.Add(); // Add a single empty page
                placeholder.Save(path);
            }
        }
        catch (Exception ex)
        {
            logAction($"Failed to create placeholder PDF: {ex.Message}");
            throw;
        }
    }
}
