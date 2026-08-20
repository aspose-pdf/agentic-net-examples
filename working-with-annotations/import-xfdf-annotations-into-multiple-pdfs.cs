using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the XFDF file containing annotation data
        string xfdfPath = "annotations.xfdf";

        // List of PDF files to which the annotations will be applied
        List<string> pdfPaths = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Directory where the processed PDFs will be saved
        string outputDir = "Output";
        Directory.CreateDirectory(outputDir);

        // Process each PDF in parallel for efficiency
        Parallel.ForEach(pdfPaths, pdfPath =>
        {
            try
            {
                // Verify that both the PDF and XFDF files exist
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"PDF not found: {pdfPath}");
                    return;
                }
                if (!File.Exists(xfdfPath))
                {
                    Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
                    return;
                }

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file into the current document
                    doc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Construct the output file path (same name, different folder)
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                    // Save the updated PDF; Save(string) always writes PDF regardless of extension
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of an individual file
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        });
    }
}