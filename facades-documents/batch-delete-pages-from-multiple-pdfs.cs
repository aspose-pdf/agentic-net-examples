using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputDirectory = Path.Combine(baseDir, "InputPdfs");
        string outputDirectory = Path.Combine(baseDir, "OutputPdfs");

        // If the input folder does not exist, fall back to the current working directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input folder '{inputDirectory}' not found. Falling back to current directory.");
            inputDirectory = Directory.GetCurrentDirectory();
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Define the pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 4 };

        // Retrieve all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'." );
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileName);

            // Verify the source file exists before trying to load it
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the PDF document inside a using block to ensure proper disposal
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Delete the specified pages in descending order to avoid index shifting
                    foreach (int pageNum in pagesToDelete.OrderByDescending(p => p))
                    {
                        if (pageNum >= 1 && pageNum <= pdfDoc.Pages.Count)
                        {
                            pdfDoc.Pages.Delete(pageNum);
                        }
                    }

                    // Save the modified document
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
