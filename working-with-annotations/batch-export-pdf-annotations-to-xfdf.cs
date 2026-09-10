using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (portable across environments)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputXfdf");

        // Ensure both directories exist – creates them if they are missing
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files from the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Place PDFs there and rerun the program.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Verify the file actually exists before processing (defensive programming)
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"Skipping missing file: {pdfPath}");
                continue;
            }

            // Build the XFDF output path using the same file name (different extension)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(outputFolder, fileNameWithoutExt + ".xfdf");

            try
            {
                // Load the PDF document and export its annotations to XFDF
                using (Document doc = new Document(pdfPath))
                {
                    doc.ExportAnnotationsToXfdf(xfdfPath);
                }

                Console.WriteLine($"Exported annotations from '{pdfPath}' to '{xfdfPath}'.");
            }
            catch (Exception ex)
            {
                // Provide a clear error message without crashing the whole batch
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
