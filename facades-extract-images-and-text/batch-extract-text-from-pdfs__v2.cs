using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

namespace PdfBatchExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adjust these paths as needed
            string inputDirectory = @"C:\PDFs";
            string outputDirectory = @"C:\ExtractedText";

            // Ensure the output directory exists
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            // Get all PDF files in the input directory (non‑recursive)
            string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

            foreach (string pdfPath in pdfFiles)
            {
                string txtPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(pdfPath) + ".txt");

                try
                {
                    // PdfExtractor implements IDisposable – use a using block to guarantee disposal
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(pdfPath);
                        extractor.ExtractText();

                        // GetText requires a destination stream. Write the extracted text to a MemoryStream
                        // and then read it back as a string.
                        using (MemoryStream textStream = new MemoryStream())
                        {
                            extractor.GetText(textStream);
                            textStream.Position = 0; // rewind
                            using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                            {
                                string extractedText = reader.ReadToEnd();
                                // Write the text to a UTF‑8 encoded file
                                File.WriteAllText(txtPath, extractedText, Encoding.UTF8);
                            }
                        }
                    }

                    Console.WriteLine($"Extracted: {Path.GetFileName(pdfPath)} → {Path.GetFileName(txtPath)}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to extract '{pdfPath}': {ex.Message}");
                }
            }
        }
    }
}
