using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Directories are supplied via environment variables or default to /data paths
            string inputDir = Environment.GetEnvironmentVariable("INPUT_DIR") ?? "/data/input";
            string outputDir = Environment.GetEnvironmentVariable("OUTPUT_DIR") ?? "/data/output";

            if (!Directory.Exists(inputDir))
            {
                Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
                return;
            }

            Directory.CreateDirectory(outputDir);

            // Process each PDF file found in the input directory
            foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
            {
                try
                {
                    ProcessPdf(pdfPath, outputDir);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }

            Console.WriteLine("PDF processing completed.");
        }

        static void ProcessPdf(string pdfPath, string outputRoot)
        {
            // Initialize PdfExtractor (lifecycle rule: use constructor)
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file (lifecycle rule: BindPdf(string))
                extractor.BindPdf(pdfPath);

                // ---- Text extraction ----
                string textOutput = Path.Combine(outputRoot,
                    Path.GetFileNameWithoutExtension(pdfPath) + ".txt");
                extractor.ExtractText();               // start text extraction
                extractor.GetText(textOutput);         // save extracted text to file

                // ---- Image extraction ----
                extractor.ExtractImage();               // start image extraction
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imageOutput = Path.Combine(outputRoot,
                        $"{Path.GetFileNameWithoutExtension(pdfPath)}_image_{imageIndex}.png");
                    // GetNextImage(string) saves the next image; format inferred from extension
                    extractor.GetNextImage(imageOutput);
                    imageIndex++;
                }

                // Explicitly close the facade (Dispose will also be called by using)
                extractor.Close();
            }
        }
    }
}
