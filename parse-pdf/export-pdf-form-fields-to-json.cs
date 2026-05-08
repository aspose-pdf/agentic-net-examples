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

        // Verify that the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output JSON file path (same base name as the PDF)
            string jsonPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + ".json");

            try
            {
                // Load the PDF document (using the standard load rule)
                using (Document doc = new Document(pdfPath))
                {
                    // Export all form fields to a JSON file.
                    // The ExportToJson overload with a file name writes the JSON directly.
                    doc.Form.ExportToJson(jsonPath);
                }

                Console.WriteLine($"Exported: {pdfPath} → {jsonPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}