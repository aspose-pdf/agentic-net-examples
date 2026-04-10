using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files (relative to the executable directory)
        const string inputFolder = "InputPdfs";
        // Folder where sanitized PDFs will be saved
        const string outputFolder = "SanitizedPdfs";

        // Resolve paths relative to the current working directory (or executable location)
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string inputPath = Path.Combine(basePath, inputFolder);
        string outputPath = Path.Combine(basePath, outputFolder);

        // Ensure the output directory exists
        Directory.CreateDirectory(outputPath);

        // Verify that the input directory exists
        if (!Directory.Exists(inputPath))
        {
            Console.WriteLine($"Input folder not found: '{inputPath}'. No PDFs to process.");
            return; // Gracefully exit – nothing to do
        }

        // Retrieve all PDF files from the input folder
        string[] pdfFiles = Directory.GetFiles(inputPath, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputPath}'.");
            return;
        }

        foreach (string sourceFile in pdfFiles)
        {
            // Preserve the original file name for the output
            string fileName = Path.GetFileName(sourceFile);
            string destinationFile = Path.Combine(outputPath, fileName);

            // Load the PDF, apply sanitization steps, and save the cleaned version
            using (Document doc = new Document(sourceFile))
            {
                // Remove document metadata (author, title, etc.)
                doc.RemoveMetadata();

                // Flatten form fields and annotations into static content
                doc.Flatten();

                // Remove PDF/A compliance if present
                doc.RemovePdfaCompliance();

                // Remove PDF/UA compliance if present
                doc.RemovePdfUaCompliance();

                // Optimize resources (remove unused objects, merge duplicates)
                doc.OptimizeResources();

                // Save the sanitized PDF to the target folder
                doc.Save(destinationFile);
            }

            Console.WriteLine($"Sanitized PDF saved: {destinationFile}");
        }
    }
}
