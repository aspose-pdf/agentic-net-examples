using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputDirectory = "input-pdfs";
        string outputDirectory = "sanitized-pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);
        // Switch to output directory so Save uses a simple filename
        string originalDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(outputDirectory);

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Remove document metadata
                    doc.RemoveMetadata();

                    // Flatten form fields (if any)
                    doc.Flatten();

                    // Remove PDF/A compliance
                    doc.RemovePdfaCompliance();

                    // Remove PDF/UA compliance
                    doc.RemovePdfUaCompliance();

                    // Optimize resources (remove unused objects, merge duplicates)
                    doc.OptimizeResources();

                    // Save sanitized PDF using the same file name in the current (output) directory
                    doc.Save(fileName);
                }

                Console.WriteLine($"Sanitized: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }

        // Restore original working directory
        Directory.SetCurrentDirectory(originalDirectory);
    }
}
