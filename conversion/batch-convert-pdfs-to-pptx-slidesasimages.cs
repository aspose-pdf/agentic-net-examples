using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where converted PPTX files will be placed
        const string outputFolder = "OutputPptx";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Build the output PPTX file name (same base name, .pptx extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputFolder, baseName + ".pptx");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Configure PPTX save options – enable rasterized slide output
                    PptxSaveOptions saveOptions = new PptxSaveOptions
                    {
                        SlidesAsImages = true
                    };

                    // Save as PPTX using the explicit save options (required for non‑PDF formats)
                    pdfDoc.Save(pptxPath, saveOptions);
                }

                Console.WriteLine($"Converted: {pdfPath} → {pptxPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}