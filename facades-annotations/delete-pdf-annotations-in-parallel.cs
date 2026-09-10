using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the input folder exists – if it does not, create it and inform the user.
        // This prevents a DirectoryNotFoundException when the sample is run out‑of‑the‑box.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder not found. Created empty folder at '{inputFolder}'. Place PDF files there and re‑run the program.");
            return; // Nothing to process yet.
        }

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Add PDFs and re‑run the program.");
            return;
        }

        // Use a degree of parallelism equal to the number of logical processors.
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        // Process each PDF file concurrently.
        Parallel.ForEach(pdfFiles, parallelOptions, pdfPath =>
        {
            try
            {
                // Determine the output file path (same file name, different folder).
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                // Use PdfAnnotationEditor from Aspose.Pdf.Facades to delete all annotations.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);          // Load the PDF.
                    editor.DeleteAnnotations();       // Remove all annotations.
                    editor.Save(outputPath);          // Save the cleaned PDF.
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors for the specific file without stopping other tasks.
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}
