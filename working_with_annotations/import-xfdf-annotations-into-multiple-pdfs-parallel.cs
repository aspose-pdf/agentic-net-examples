using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files
        List<string> pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
            // add more paths as needed
        };

        // XFDF file containing annotations to import
        const string xfdfPath = "annotations.xfdf";

        // Verify XFDF file exists
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                    return;
                }

                // Determine output file name (original name with suffix)
                string directory = Path.GetDirectoryName(pdfPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(directory ?? string.Empty,
                    $"{fileNameWithoutExt}_annotated.pdf");

                // Load, import annotations, and save
                using (Document doc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file
                    doc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Save the updated PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}