using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class BatchPdfToPptx
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = @"C:\PdfInput";
        // Directory where the resulting PPTX files will be saved
        const string outputDirectory = @"C:\PptxOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Derive the output PPTX file name from the PDF file name
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pptx");

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize PPTX save options and enable rasterized slide output
                PptxSaveOptions saveOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true // each slide will be rendered as an image
                };

                // Save the document as PPTX using the explicit save options
                pdfDocument.Save(pptxPath, saveOptions);
            }

            Console.WriteLine($"Converted '{pdfPath}' → '{pptxPath}'");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}