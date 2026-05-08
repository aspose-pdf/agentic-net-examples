using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfProcessingService
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input and output directories are expected to be mounted as volumes.
            // They can be overridden with environment variables.
            string inputDirectory  = Environment.GetEnvironmentVariable("INPUT_DIR")  ?? "/data/input";
            string outputDirectory = Environment.GetEnvironmentVariable("OUTPUT_DIR") ?? "/data/output";

            // Ensure the input directory exists – create it if the volume is missing.
            // If creation fails (read‑only volume), fall back to a temporary folder.
            inputDirectory = EnsureReadableDirectory(inputDirectory);

            // Ensure the output directory is writable.
            outputDirectory = EnsureWritableDirectory(outputDirectory);

            // Process each PDF file found in the input directory.
            foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
            {
                // Derive a text file name based on the PDF file name.
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string txtPath = Path.Combine(outputDirectory, $"{fileNameWithoutExt}.txt");

                // Use PdfExtractor (Aspose.Pdf.Facades) to extract text.
                // The extractor implements IDisposable via the Facade base class, so we wrap it in a using block.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file to the extractor.
                    extractor.BindPdf(pdfPath);

                    // Extract all text from the document using Unicode encoding (default).
                    extractor.ExtractText();

                    // Save the extracted text to the target .txt file.
                    extractor.GetText(txtPath);
                }

                Console.WriteLine($"Extracted text from '{pdfPath}' to '{txtPath}'.");
            }

            Console.WriteLine("PDF processing completed.");
        }

        /// <summary>
        /// Tries to create the requested directory. If creation fails because the path is read‑only,
        /// a writable temporary directory is created and returned.
        /// </summary>
        private static string EnsureWritableDirectory(string desiredPath)
        {
            try
            {
                Directory.CreateDirectory(desiredPath);
                return desiredPath;
            }
            catch (IOException)
            {
                // The filesystem is likely read‑only. Use the OS temporary folder instead.
                return CreateTempFallback();
            }
            catch (UnauthorizedAccessException)
            {
                // Same fallback for permission issues.
                return CreateTempFallback();
            }
        }

        /// <summary>
        /// Tries to ensure the input directory exists. If it cannot be created (read‑only volume),
        /// falls back to a temporary folder so the service can still start without crashing.
        /// </summary>
        private static string EnsureReadableDirectory(string desiredPath)
        {
            try
            {
                Directory.CreateDirectory(desiredPath);
                return desiredPath;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                string tempPath = CreateTempFallback();
                Console.WriteLine($"Warning: Could not access input directory '{desiredPath}' ({ex.Message}). Falling back to temporary directory '{tempPath}'.");
                return tempPath;
            }
        }

        private static string CreateTempFallback()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "pdf_io");
            Directory.CreateDirectory(tempPath);
            Console.WriteLine($"Info: Using temporary directory '{tempPath}'.");
            return tempPath;
        }
    }
}
