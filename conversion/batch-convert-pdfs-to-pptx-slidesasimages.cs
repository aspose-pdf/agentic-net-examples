using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDirectory = "InputPdfs";
        // Directory where converted PPTX files will be placed
        const string outputDirectory = "OutputPptx";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Retrieve all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Build the output PPTX file name (same base name, .pptx extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputDirectory, baseName + ".pptx");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Create PPTX save options and enable rasterized slide output
                    PptxSaveOptions saveOptions = new PptxSaveOptions
                    {
                        SlidesAsImages = true
                    };

                    // Save the document as PPTX using the explicit save options
                    pdfDocument.Save(pptxPath, saveOptions);
                }

                Console.WriteLine($"Converted: '{pdfPath}' → '{pptxPath}'");
            }
            catch (Exception ex)
            {
                // Report any conversion errors but continue processing remaining files
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}