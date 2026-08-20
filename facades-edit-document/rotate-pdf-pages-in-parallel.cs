using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputDirectory = Path.Combine(baseDir, "InputPdfs");
        string outputDirectory = Path.Combine(baseDir, "OutputPdfs");

        // Validate input folder – if it does not exist, fall back to the current working directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input folder '{inputDirectory}' not found. Falling back to current directory.");
            inputDirectory = Directory.GetCurrentDirectory();
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Gather all PDF files from the input folder
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'. Nothing to process.");
            return;
        }

        // Process each PDF in parallel to improve performance
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Verify the file still exists (it could have been moved/deleted between enumeration and processing)
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    return;
                }

                // Build the output file name (e.g., original_rotated.pdf)
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_rotated.pdf");

                // Use PdfPageEditor (Aspose.Pdf.Facades) to rotate all pages by 90 degrees
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(pdfPath);
                    // Set rotation for all pages (valid values: 0, 90, 180, 270)
                    editor.Rotation = 90;
                    editor.ApplyChanges();
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Log the error but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs have been processed.");
    }
}
