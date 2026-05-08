using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source PDFs
        const string inputDir = "InputPdfs";
        // Directory where the processed PDFs will be saved
        const string outputDir = "OutputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files (including subfolders)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.AllDirectories);

        // Process each PDF in parallel to delete its annotations
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Preserve relative folder structure in the output directory
                string relativePath = Path.GetRelativePath(inputDir, pdfPath);
                string outputPath = Path.Combine(outputDir, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                // Use PdfAnnotationEditor to load, modify, and save the PDF
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the PDF file
                    editor.BindPdf(pdfPath);
                    // Delete all annotations in the document
                    editor.DeleteAnnotations();
                    // Save the cleaned PDF to the output location
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs have been processed.");
    }
}