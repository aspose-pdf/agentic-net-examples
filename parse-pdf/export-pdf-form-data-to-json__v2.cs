using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "InputPdfs";

        // Folder where the JSON files will be written
        const string outputFolder = "FormJson";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the JSON file name – same base name as the PDF
                string jsonFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".json";
                string jsonPath = Path.Combine(outputFolder, jsonFileName);

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Export the form fields of the document to the JSON file
                    // ExportToJson(string) writes the JSON directly to the specified path
                    doc.Form.ExportToJson(jsonPath);
                }

                Console.WriteLine($"Exported form data: {Path.GetFileName(pdfPath)} → {Path.GetFileName(jsonPath)}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
