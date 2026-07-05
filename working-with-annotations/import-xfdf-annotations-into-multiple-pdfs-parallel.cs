using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files – replace with your actual file paths
        List<string> pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // XFDF file containing the annotations to import
        const string xfdfPath = "annotations.xfdf";

        // Directory where the annotated PDFs will be saved
        const string outputDirectory = "AnnotatedOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Verify that the XFDF file exists before starting the parallel work
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Process each PDF in parallel for maximum efficiency
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Skip missing PDF files but continue processing others
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                    return;
                }

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file into the current document
                    doc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Build the output file path (preserve original file name)
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(pdfPath));

                    // Save the modified document (PDF format is the default)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Annotations imported and saved: {pdfPath} -> {outputDirectory}");
            }
            catch (Exception ex)
            {
                // Log any errors for the specific file without aborting the whole operation
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}