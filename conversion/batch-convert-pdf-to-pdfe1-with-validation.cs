using System;
using System.IO;
using Aspose.Pdf;

class PdfE1BatchConverter
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Resolve input and output folders. If the expected folders are missing,
        // fall back to the current working directory so the sample can run out‑
        // of‑the‑box without throwing a DirectoryNotFoundException.
        // ---------------------------------------------------------------------
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string sourceFolder = Path.Combine(baseDir, "sourceFolderC");
        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Source folder '{sourceFolder}' not found. Using current directory as fallback.");
            sourceFolder = Directory.GetCurrentDirectory();
        }

        string outputFolder = Path.Combine(baseDir, "outputFolder");
        Directory.CreateDirectory(outputFolder);
        string reportsFolder = Path.Combine(outputFolder, "Reports");
        Directory.CreateDirectory(reportsFolder);

        // ---------------------------------------------------------------------
        // Process each PDF file found in the source folder.
        // ---------------------------------------------------------------------
        foreach (string inputPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string convertedPath = Path.Combine(outputFolder, fileNameWithoutExt + "_PDFE1.pdf");
            string reportPath = Path.Combine(reportsFolder, fileNameWithoutExt + "_validation.xml");

            try
            {
                // Load the source PDF. The using block guarantees deterministic disposal.
                using (Document doc = new Document(inputPath))
                {
                    // Convert to PDF/E‑1. The conversion log is written to the same report file.
                    doc.Convert(reportPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                    // Save the converted PDF/E‑1 document.
                    doc.Save(convertedPath);

                    // Validate the saved PDF/E‑1 file and write a detailed XML report.
                    bool isValid = doc.Validate(reportPath, PdfFormat.PDF_E_1);
                    Console.WriteLine($"{fileNameWithoutExt}: Validation {(isValid ? "passed" : "failed")} – report saved to '{reportPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion and validation completed.");
    }
}
