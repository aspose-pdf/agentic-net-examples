using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Directories can be overridden with environment variables
        string inputDir = Environment.GetEnvironmentVariable("PDF_INPUT_DIR") ?? "/data/input";
        string outputDir = Environment.GetEnvironmentVariable("PDF_OUTPUT_DIR") ?? "/data/output";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Process each PDF file found in the mounted input volume
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string txtPath = Path.Combine(outputDir, baseName + ".txt");

            try
            {
                // Use PdfExtractor from Aspose.Pdf.Facades
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the source PDF file
                    extractor.BindPdf(pdfPath);

                    // Extract all text from the document
                    extractor.ExtractText();

                    // Write the extracted text to a .txt file
                    extractor.GetText(txtPath);
                }

                Console.WriteLine($"Extracted: {pdfPath} -> {txtPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
