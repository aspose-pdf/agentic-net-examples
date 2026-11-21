using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfAnnotationEditor resides here

class Program
{
    static void Main()
    {
        // Directory containing PDF files
        const string inputDirectory = "pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Get all PDF files in the directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Create the annotation editor (no IDisposable implementation)
                Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();

                // Load the PDF file
                editor.BindPdf(pdfPath);

                // Delete all annotations of type "Stamp"
                editor.DeleteAnnotations("Stamp");

                // Save the modified PDF back to the same file
                editor.Save(pdfPath);

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}