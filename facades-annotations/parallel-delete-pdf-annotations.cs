using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDFs to process
        const string inputDirectory = @"C:\PdfFiles";
        // Directory where the cleaned PDFs will be saved
        const string outputDirectory = @"C:\PdfFiles\Cleaned";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Gather all PDF file paths
        List<string> pdfFiles = new List<string>();
        foreach (string file in Directory.EnumerateFiles(inputDirectory, "*.pdf", SearchOption.AllDirectories))
        {
            pdfFiles.Add(file);
        }

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Determine output file path (preserve relative structure)
                string relativePath = Path.GetRelativePath(inputDirectory, pdfPath);
                string outputPath = Path.Combine(outputDirectory, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                // Use PdfAnnotationEditor to delete all annotations
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(pdfPath);               // Load the PDF
                editor.DeleteAnnotations();            // Remove every annotation
                editor.Save(outputPath);               // Save the cleaned PDF
                editor.Close();                       // Release resources
            }
            catch (Exception ex)
            {
                // Log error – in a real scenario replace with proper logging
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Annotation removal completed.");
    }
}