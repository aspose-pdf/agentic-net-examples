using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly two arguments: input directory and output directory
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: PdfBatchExtract <InputDirectory> <OutputDirectory>");
            return;
        }

        string inputDirectory = args[0];
        string outputDirectory = args[1];

        // Validate input directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Error: Input directory does not exist -> {inputDirectory}");
            return;
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Get all PDF files (non‑recursive) in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string txtFilePath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(pdfPath) + ".txt");

            // Use PdfExtractor inside a using block to guarantee disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();
                extractor.GetText(txtFilePath);
            }

            Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfPath)}' to '{txtFilePath}'");
        }
    }
}
