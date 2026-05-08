using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs and their matching JSON files
        const string inputDir = @"C:\InputFiles";
        // Output directory where the filled PDFs will be saved
        const string outputDir = @"C:\OutputFiles";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Build a list of PDF files that have a corresponding JSON file
        var pdfFiles = new List<string>();
        foreach (string pdfPath in Directory.EnumerateFiles(inputDir, "*.pdf"))
        {
            string jsonPath = Path.ChangeExtension(pdfPath, ".json");
            if (File.Exists(jsonPath))
                pdfFiles.Add(pdfPath);
        }

        // Process each PDF/JSON pair in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            string jsonPath = Path.ChangeExtension(pdfPath, ".json");
            string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

            try
            {
                // Bind the PDF to a Form facade
                using (Form form = new Form(pdfPath))
                {
                    // Import JSON data
                    using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                    {
                        form.ImportJson(jsonStream);
                    }

                    // Save the updated PDF
                    form.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch import completed.");
    }
}