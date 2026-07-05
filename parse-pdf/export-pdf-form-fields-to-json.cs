using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = "InputPdfs";

        // Folder where the JSON files will be written
        const string outputFolder = "OutputJson";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the corresponding JSON file name
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string jsonPath = Path.Combine(outputFolder, baseName + ".json");

            try
            {
                // Load the PDF document (lifecycle managed by using)
                using (Document doc = new Document(pdfPath))
                {
                    // Export all form fields to a JSON file
                    // ExportToJson(string) writes directly to the specified file
                    doc.Form.ExportToJson(jsonPath);
                }

                Console.WriteLine($"Exported: {pdfPath} -> {jsonPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}