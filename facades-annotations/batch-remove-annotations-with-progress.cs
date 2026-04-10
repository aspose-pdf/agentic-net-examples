using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\PdfBatch\Input";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        int totalFiles = pdfFiles.Length;

        if (totalFiles == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        for (int i = 0; i < totalFiles; i++)
        {
            string inputPath  = pdfFiles[i];
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_cleaned.pdf");

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove all annotations from every page
                foreach (Page page in doc.Pages)
                {
                    // Annotations collection can be null for pages without annotations
                    page.Annotations?.Clear();
                }

                // Save the modified document to the output path
                doc.Save(outputPath);
            }

            // Update progress bar
            int processed = i + 1;
            double percent = (processed / (double)totalFiles) * 100;
            Console.Write($"\rProcessed {processed}/{totalFiles} ({percent:0.##}%)");
        }

        Console.WriteLine("\nBatch annotation deletion completed.");
    }
}
