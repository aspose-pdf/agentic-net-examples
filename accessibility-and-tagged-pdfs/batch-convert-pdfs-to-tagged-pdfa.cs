using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (works cross‑platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input folder containing untagged PDFs
        string inputFolder = Path.Combine(baseDir, "input_pdfs");
        // Output folder for tagged PDFs
        string outputFolder = Path.Combine(baseDir, "tagged_pdfs");

        // Ensure the input and output directories exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder not found. Created empty folder at '{inputFolder}'. Place PDFs there and re‑run.");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Enable auto‑tagging globally (static default instance)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_tagged.pdf");

            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Prepare conversion options with auto‑tagging enabled
                    PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
                    options.AutoTaggingSettings = AutoTaggingSettings.Default; // use the static default

                    // Convert the document; this applies auto‑tagging and produces a tagged PDF/A
                    doc.Convert(options);

                    // Save the newly tagged PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted '{inputPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
