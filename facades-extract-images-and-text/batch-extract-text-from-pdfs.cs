using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where extracted text files will be saved
        const string outputFolder = "ExtractedTexts";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string txtPath = Path.Combine(outputFolder, baseName + ".txt");

            try
            {
                // PdfExtractor implements IDisposable, so use a using block
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Load the PDF file into the extractor
                    extractor.BindPdf(pdfPath);
                    // Extract all text (Unicode encoding is default)
                    extractor.ExtractText();
                    // Write the extracted text to a .txt file with the same base name
                    extractor.GetText(txtPath);
                }

                Console.WriteLine($"Extracted text: '{pdfPath}' → '{txtPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}