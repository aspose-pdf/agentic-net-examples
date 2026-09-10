using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where JSON files will be written
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
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Build the JSON output path (same base name, .json extension)
                    string jsonFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".json";
                    string jsonPath = Path.Combine(outputFolder, jsonFileName);

                    // Export the entire form to a JSON file (Form.ExportToJson overload)
                    doc.Form.ExportToJson(jsonPath);
                }

                Console.WriteLine($"Exported: {Path.GetFileName(pdfPath)} → {Path.GetFileNameWithoutExtension(pdfPath)}.json");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}