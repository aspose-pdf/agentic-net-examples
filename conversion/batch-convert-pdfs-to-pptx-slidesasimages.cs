using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where converted PPTX files will be saved
        const string outputFolder = "OutputPpts";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Build output PPTX file path (same base name)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputFolder, baseName + ".pptx");

            // Load PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Initialize PPTX save options and enable rasterized slide output
                PptxSaveOptions saveOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true // each slide will be an image of the PDF page
                };

                // Save the PDF as PPTX using the configured options
                pdfDoc.Save(pptxPath, saveOptions);
            }

            Console.WriteLine($"Converted '{pdfPath}' → '{pptxPath}'.");
        }
    }
}