using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class PdfFormExtractor
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = @"C:\InputPdfs";

        // Folder where the JSON files will be written
        const string outputFolder = @"C:\FormJson";

        // Verify that the input directory exists before trying to enumerate files.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            // Optionally, you could create the folder or exit with a non‑zero code.
            return; // Gracefully exit the program.
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Derive a JSON file name that matches the PDF name
            string jsonFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".json";
            string jsonPath = Path.Combine(outputFolder, jsonFileName);

            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export all form fields to a JSON file.
                // The ExportToJson overload without options uses default settings.
                doc.Form.ExportToJson(jsonPath);
            }

            Console.WriteLine($"Exported form data from '{pdfPath}' to '{jsonPath}'.");
        }
    }
}
