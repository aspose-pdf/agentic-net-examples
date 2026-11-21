using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (cross‑platform safe)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input folders (relative to the base directory)
        string jsonFolder = Path.Combine(baseDir, "JsonData");
        string pdfFolder = Path.Combine(baseDir, "PdfTemplates");
        string outputFolder = Path.Combine(baseDir, "PdfFilled");

        // Ensure folders exist – if a folder is missing we fall back to the current directory
        if (!Directory.Exists(jsonFolder))
        {
            Console.Error.WriteLine($"JSON folder not found: {jsonFolder}. Using current directory instead.");
            jsonFolder = Directory.GetCurrentDirectory();
        }
        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"PDF template folder not found: {pdfFolder}. Using current directory instead.");
            pdfFolder = Directory.GetCurrentDirectory();
        }
        Directory.CreateDirectory(outputFolder);

        // Collect all JSON files
        string[] jsonFiles = Directory.GetFiles(jsonFolder, "*.json");
        if (jsonFiles.Length == 0)
        {
            Console.WriteLine("No JSON files found – nothing to process.");
            return;
        }

        // Process each pair in parallel
        Parallel.ForEach(jsonFiles, jsonPath =>
        {
            try
            {
                // Derive PDF name from JSON file name
                string baseName = Path.GetFileNameWithoutExtension(jsonPath);
                string pdfPath = Path.Combine(pdfFolder, baseName + ".pdf");

                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"PDF not found for JSON: {jsonPath}");
                    return;
                }

                string outputPath = Path.Combine(outputFolder, baseName + ".pdf");

                // Use the Form facade to bind the PDF, import JSON data, and save
                using (var form = new Aspose.Pdf.Facades.Form())
                {
                    form.BindPdf(pdfPath);
                    // ImportJson expects a Stream, so open the JSON file as a FileStream
                    using (FileStream jsonStream = File.OpenRead(jsonPath))
                    {
                        form.ImportJson(jsonStream);
                    }
                    form.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{jsonPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch import of form data completed.");
    }
}
