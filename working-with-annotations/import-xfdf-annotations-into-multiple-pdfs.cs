using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input / output folders relative to the base directory
        string pdfFolder = Path.Combine(baseDir, "pdfs");
        string xfdfFolder = Path.Combine(baseDir, "xfdf");
        string outputFolder = Path.Combine(baseDir, "output");

        // If the expected folders are missing, fall back to the current working directory
        if (!Directory.Exists(pdfFolder))
        {
            Console.WriteLine($"PDF folder not found: {pdfFolder}. Using current directory instead.");
            pdfFolder = Directory.GetCurrentDirectory();
        }
        if (!Directory.Exists(xfdfFolder))
        {
            Console.WriteLine($"XFDF folder not found: {xfdfFolder}. Using current directory instead.");
            xfdfFolder = Directory.GetCurrentDirectory();
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files from the resolved folder
        string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF in parallel for maximum efficiency
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string xfdfPath = Path.Combine(xfdfFolder, baseName + ".xfdf");
                string outputPath = Path.Combine(outputFolder, baseName + "_annotated.pdf");

                // Skip if the corresponding XFDF file does not exist
                if (!File.Exists(xfdfPath))
                {
                    Console.Error.WriteLine($"XFDF not found for '{baseName}'. Skipping.");
                    return;
                }

                // Load the PDF, import annotations from XFDF, and save the result
                using (Document doc = new Document(pdfPath))
                {
                    doc.ImportAnnotationsFromXfdf(xfdfPath);
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Successfully processed '{baseName}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}
