using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: Program <InputFolder> [OutputFolder]");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(inputFolder, "ExtractedText");

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each PDF file in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfPath in pdfFiles)
        {
            string txtPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(pdfPath) + ".txt");

            // Use PdfExtractor to extract text
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();
                extractor.GetText(txtPath);
            }

            Console.WriteLine($"Extracted text from '{pdfPath}' to '{txtPath}'");
        }
    }
}