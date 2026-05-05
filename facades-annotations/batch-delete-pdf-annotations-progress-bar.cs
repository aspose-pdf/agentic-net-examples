using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfBatchProcessing
{
    class Program
    {
        static void Main()
        {
            // Folder containing PDFs to process
            const string inputFolder = "InputPdfs";
            // Folder where cleaned PDFs will be saved
            const string outputFolder = "OutputPdfs";

            if (!Directory.Exists(inputFolder))
            {
                Console.Error.WriteLine($"Input folder not found: {inputFolder}");
                return;
            }

            Directory.CreateDirectory(outputFolder);

            // Get all PDF files in the input folder
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
            int totalFiles = pdfFiles.Length;

            if (totalFiles == 0)
            {
                Console.WriteLine("No PDF files found to process.");
                return;
            }

            // Simple console progress bar settings
            const int progressBarWidth = 50;

            for (int i = 0; i < totalFiles; i++)
            {
                string inputPath = pdfFiles[i];
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_clean.pdf");

                try
                {
                    // Use "using" to ensure resources are released promptly
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(inputPath);
                        editor.DeleteAnnotations();
                        editor.Save(outputPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                    continue;
                }

                // Update progress bar
                int processed = i + 1;
                double percent = (double)processed / totalFiles;
                int filledBars = (int)Math.Round(percent * progressBarWidth);
                string bar = new string('█', filledBars).PadRight(progressBarWidth, '░');
                Console.Write($"\rProcessing: [{bar}] {processed}/{totalFiles} ({percent:P2})");
            }

            // Move to next line after the loop so the final message appears on a new line
            Console.WriteLine();
            Console.WriteLine("Batch annotation deletion completed.");
        }
    }
}
