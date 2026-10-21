using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point: expects one argument – the folder containing PDF files
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: DeleteAnnotations.exe <folderPath>");
            return;
        }

        string folderPath = args[0];

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the specified folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the folder.");
            return;
        }

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Create a temporary file to hold the result without annotations
                string tempPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(pdfPath) + "_noann.pdf");

                // Use PdfAnnotationEditor to delete all annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(pdfPath);

                    // Delete every annotation in the document
                    editor.DeleteAnnotations();

                    // Optionally flatten remaining annotation objects (e.g., form fields) to plain content
                    editor.FlatteningAnnotations();

                    // Save the cleaned PDF to the temporary location
                    editor.Save(tempPath);
                }

                // Replace the original file with the annotation‑free version
                File.Delete(pdfPath);
                File.Move(tempPath, pdfPath);

                Console.WriteLine($"Annotations removed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
