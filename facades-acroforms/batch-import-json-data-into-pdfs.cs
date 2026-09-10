using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files and their matching JSON files
        const string inputFolder = @"C:\InputFiles";
        // Folder where the PDFs with imported data will be saved
        const string outputFolder = @"C:\OutputFiles";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        var workItems = new List<(string pdfPath, string jsonPath, string outputPath)>();

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string jsonPath = Path.Combine(inputFolder, baseName + ".json");

            // Only process if a matching JSON file exists
            if (File.Exists(jsonPath))
            {
                string outputPath = Path.Combine(outputFolder, baseName + "_filled.pdf");
                workItems.Add((pdfPath, jsonPath, outputPath));
            }
            else
            {
                Console.Error.WriteLine($"Warning: No JSON file found for '{pdfPath}'. Skipping.");
            }
        }

        // Process each PDF/JSON pair in parallel
        Parallel.ForEach(workItems, item =>
        {
            try
            {
                // Initialize the Form facade with input and output PDF paths
                using (Form form = new Form(item.pdfPath, item.outputPath))
                {
                    // Open the JSON file as a read‑only stream
                    using (FileStream jsonStream = new FileStream(item.jsonPath, FileMode.Open, FileAccess.Read))
                    {
                        // Import all form field values from the JSON stream
                        form.ImportJson(jsonStream);
                    }

                    // Save the modified PDF (output path was supplied in the constructor)
                    form.Save();
                }

                Console.WriteLine($"Successfully imported data into '{item.outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{item.pdfPath}': {ex.Message}");
            }
        });
    }
}