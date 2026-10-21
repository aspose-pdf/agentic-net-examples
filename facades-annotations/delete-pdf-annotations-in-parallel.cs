using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to process
        const string inputFolder = @"C:\PdfFiles";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Gather all PDF file paths
        List<string> pdfFiles = new List<string>(Directory.GetFiles(inputFolder, "*.pdf", SearchOption.AllDirectories));

        if (pdfFiles.Count == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Parallel options – limit degree of parallelism if needed
        ParallelOptions options = new ParallelOptions
        {
            // MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        // Process each PDF concurrently
        Parallel.ForEach(pdfFiles, options, pdfPath =>
        {
            try
            {
                // Each thread uses its own PdfAnnotationEditor instance (not IDisposable)
                PdfAnnotationEditor editor = new PdfAnnotationEditor();

                // Delete all annotations from the PDF file
                editor.DeleteAnnotations(pdfPath);

                Console.WriteLine($"Annotations removed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        });

        Console.WriteLine("Processing complete.");
    }
}