using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where PPTX files will be saved
        const string outputFolder = "OutputPptx";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputFolder, baseName + ".pptx");

            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Configure PPTX save options to rasterize each slide as an image
                    PptxSaveOptions pptxOptions = new PptxSaveOptions
                    {
                        SlidesAsImages = true
                    };

                    // Save as PPTX using the explicit save options (required for non‑PDF formats)
                    doc.Save(pptxPath, pptxOptions);
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