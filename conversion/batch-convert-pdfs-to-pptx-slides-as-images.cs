using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (portable across environments)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output directories – fall back to the current working directory if they do not exist
        string inputDirectory = Path.Combine(baseDir, "InputPdfs");
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' not found. Using current directory as fallback.");
            inputDirectory = Directory.GetCurrentDirectory();
        }

        string outputDirectory = Path.Combine(baseDir, "OutputPptx");
        // Ensure the output directory exists (creates it if missing)
        Directory.CreateDirectory(outputDirectory);

        // Retrieve PDF files – if none are found, inform the user and exit gracefully
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'. Nothing to convert.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Verify the file still exists before processing (defensive check)
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}. Skipping.");
                continue;
            }

            // Derive the output PPTX file path (same file name, .pptx extension)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pptx");

            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Configure PPTX save options with SlidesAsImages enabled
                    PptxSaveOptions saveOptions = new PptxSaveOptions
                    {
                        SlidesAsImages = true
                    };

                    // Save the document as PPTX using the explicit save options
                    pdfDocument.Save(pptxPath, saveOptions);
                }

                Console.WriteLine($"Converted '{pdfPath}' to '{pptxPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}
