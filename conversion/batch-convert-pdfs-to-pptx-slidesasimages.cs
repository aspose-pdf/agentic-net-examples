using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfToPptxConverter
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDirectory = @"C:\InputPdfs";
        // Directory where PPTX files will be saved
        const string outputDirectory = @"C:\OutputPptx";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found in the input directory.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Derive output PPTX file name
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pptx");

            try
            {
                // Load PDF document inside a using block for deterministic disposal
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Initialize PPTX save options and enable rasterized slide output
                    PptxSaveOptions pptxOptions = new PptxSaveOptions
                    {
                        SlidesAsImages = true
                    };

                    // Save the PDF as PPTX using the specified options
                    pdfDocument.Save(pptxPath, pptxOptions);
                }

                Console.WriteLine($"Converted '{pdfPath}' to '{pptxPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{pdfPath}': {ex.Message}");
            }
        }
    }
}