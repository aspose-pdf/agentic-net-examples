using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        string inputDir = "InputPdfs";
        // Directory where the generated PPTX files will be placed
        string outputDir = "OutputPptx";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Retrieve all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to convert.");
            return;
        }

        // Configure PPTX save options – rasterize each slide as an image
        PptxSaveOptions pptxOptions = new PptxSaveOptions
        {
            SlidesAsImages = true
        };

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputDir, baseName + ".pptx");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Save the document as PPTX using the configured options
                    pdfDoc.Save(pptxPath, pptxOptions);
                }

                Console.WriteLine($"Converted: {pdfPath} → {pptxPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}