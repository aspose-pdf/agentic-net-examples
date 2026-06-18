using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source PDFs
        string inputDirectory = "InputPdfs";
        // Directory where cleaned PDFs will be saved
        string outputDirectory = "OutputPdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files (including subfolders)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.AllDirectories);

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Build output file name (e.g., original_clean.pdf)
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileNameWithoutExt}_clean.pdf");

                // Use PdfAnnotationEditor to delete all annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);          // Load the PDF
                    editor.DeleteAnnotations();       // Remove every annotation
                    editor.Save(outputPath);          // Save the cleaned PDF
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Annotation removal completed for all files.");
    }
}