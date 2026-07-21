using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "InputPdfs";

        // Folder where the JSON files will be written
        const string outputFolder = "FormJson";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the JSON file name and full path **before** opening the document
                string jsonFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".json";
                string jsonPath = Path.Combine(outputFolder, jsonFileName);

                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Export the entire form to a JSON file
                    doc.Form.ExportToJson(jsonPath);
                }

                Console.WriteLine($"Exported: {Path.GetFileName(pdfPath)} → {Path.GetFileName(jsonPath)}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}