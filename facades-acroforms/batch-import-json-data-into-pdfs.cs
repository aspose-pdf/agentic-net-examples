using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs and matching JSON files
        const string inputDir = @"C:\InputFiles";
        // Output directory for PDFs with imported data
        const string outputDir = @"C:\OutputFiles";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Gather all PDF files
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each PDF (and its JSON) in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Determine corresponding JSON file (same base name)
                string jsonPath = Path.ChangeExtension(pdfPath, ".json");
                if (!File.Exists(jsonPath))
                {
                    Console.Error.WriteLine($"JSON file missing for PDF: {Path.GetFileName(pdfPath)}");
                    return;
                }

                // Prepare output PDF path
                string outputPdfPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                // Import JSON data into the PDF using Aspose.Pdf.Facades.Form
                using (Form form = new Form(pdfPath, outputPdfPath))
                {
                    using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                    {
                        form.ImportJson(jsonStream);
                    }
                    form.Save(); // Save the modified PDF
                }

                Console.WriteLine($"Imported data from '{Path.GetFileName(jsonPath)}' into '{Path.GetFileName(outputPdfPath)}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        });
    }
}