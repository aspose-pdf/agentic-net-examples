using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files and matching JSON files
        const string inputDir = @"C:\Input";
        // Output directory for PDFs after importing JSON data
        const string outputDir = @"C:\Output";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Collect pairs of PDF and JSON files (same base name)
        var filePairs = new List<(string PdfPath, string JsonPath)>();
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string jsonPath = Path.ChangeExtension(pdfPath, ".json");
            if (File.Exists(jsonPath))
            {
                filePairs.Add((pdfPath, jsonPath));
            }
            else
            {
                Console.WriteLine($"Warning: No JSON file for '{pdfPath}'. Skipping.");
            }
        }

        if (filePairs.Count == 0)
        {
            Console.WriteLine("No PDF/JSON pairs found.");
            return;
        }

        // Process each pair in parallel
        Parallel.ForEach(filePairs, pair =>
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pair.PdfPath))
                {
                    // Initialize the Facades Form on the loaded document
                    using (Form form = new Form(doc))
                    {
                        // Open the JSON file as a stream and import data
                        using (FileStream jsonStream = new FileStream(pair.JsonPath, FileMode.Open, FileAccess.Read))
                        {
                            form.ImportJson(jsonStream);
                        }

                        // Determine output path (same file name in output directory)
                        string outputPath = Path.Combine(outputDir, Path.GetFileName(pair.PdfPath));

                        // Save the modified PDF
                        doc.Save(outputPath);
                    }
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pair.PdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pair.PdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch import completed.");
    }
}