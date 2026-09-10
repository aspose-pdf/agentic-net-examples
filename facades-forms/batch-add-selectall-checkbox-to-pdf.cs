using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input / output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the folders exist – if the input folder is missing we create it and inform the user.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"[Info] Input folder not found. Created empty folder at '{inputFolder}'. Place PDF files there and re‑run the program.");
            return; // Nothing to process yet.
        }
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"[Info] No PDF files found in '{inputFolder}'. Add PDFs and run again.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build the output file path (adds a suffix to avoid overwriting the original)
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_with_checkbox.pdf");

                // Use FormEditor (a Facades class) to add a checkbox field
                using (FormEditor formEditor = new FormEditor())
                {
                    // Bind the source PDF file
                    formEditor.BindPdf(inputPath);

                    // Add a checkbox named "SelectAll" on the first page.
                    // Coordinates are in points (lower‑left x, lower‑left y, upper‑right x, upper‑right y).
                    // Example rectangle: (50,750) to (70,770) places the box near the top‑left corner.
                    formEditor.AddField(FieldType.CheckBox, "SelectAll", 1, 50, 750, 70, 770);

                    // Save the modified PDF to the designated output path
                    formEditor.Save(outputPath);
                }

                Console.WriteLine($"[Success] Processed: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to process '{inputPath}'. Exception: {ex.Message}");
            }
        }
    }
}
