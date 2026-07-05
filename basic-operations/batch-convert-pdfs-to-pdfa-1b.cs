using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs
        const string inputFolder = "InputPdfs";
        // Output folder for PDF/A‑1b files
        const string outputFolder = "OutputPdfA";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
            string targetPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_pdfa.pdf");
            string logPath    = Path.Combine(outputFolder, $"{fileNameWithoutExt}_log.txt");

            try
            {
                // Load the source PDF (using block ensures proper disposal)
                using (Document doc = new Document(sourcePath))
                {
                    // Convert to PDF/A‑1b; errors are written to the log file
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                    // Save the converted document (still a PDF file)
                    doc.Save(targetPath);
                }

                Console.WriteLine($"Converted: {sourcePath} → {targetPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{sourcePath}': {ex.Message}");
            }
        }
    }
}