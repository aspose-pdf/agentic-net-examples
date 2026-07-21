using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to process
        const string inputDirectory  = @"C:\PdfInput";
        // Directory where processed PDFs will be saved (can be the same as input for in‑place overwrite)
        const string outputDirectory = @"C:\PdfOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each file in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Determine output file path (overwrite original if desired)
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(pdfPath));

                // Create the annotation editor facade
                PdfAnnotationEditor editor = new PdfAnnotationEditor();

                // Bind the PDF file to the editor
                editor.BindPdf(pdfPath);

                // Delete all annotations in the document
                editor.DeleteAnnotations();

                // Save the modified PDF
                editor.Save(outputPath);

                // Release resources held by the facade
                editor.Close();

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                // Log any errors for this file without stopping other parallel tasks
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Annotation removal completed.");
    }
}