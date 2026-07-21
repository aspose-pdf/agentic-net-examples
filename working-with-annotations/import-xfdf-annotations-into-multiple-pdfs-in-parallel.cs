using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input: list of PDF files to process
        List<string> pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
            // add more file paths as needed
        };

        // Path to the XFDF file containing annotation data
        const string xfdfPath = "annotations.xfdf";

        // Directory where processed PDFs will be saved
        const string outputDir = "ProcessedPdfs";

        // Validate inputs
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each PDF in parallel for efficiency
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                    return;
                }

                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file into the current document
                    doc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Build output file path (preserve original file name)
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed and saved: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}