using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where extracted text files will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Derive the output text file name (same base name, .txt extension)
            string txtFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".txt";
            string txtPath = Path.Combine(outputFolder, txtFileName);

            try
            {
                // PdfExtractor implements IDisposable, so wrap it in a using block
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF document to the extractor
                    extractor.BindPdf(pdfPath);

                    // Enable text extraction
                    extractor.ExtractText();

                    // Retrieve the extracted text via the Stream overload
                    string extractedText;
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream); // write text to the stream
                        textStream.Position = 0;        // rewind for reading
                        using (StreamReader reader = new StreamReader(textStream))
                        {
                            extractedText = reader.ReadToEnd();
                        }
                    }

                    // Write the text to the output file (UTF‑8)
                    File.WriteAllText(txtPath, extractedText);
                }

                Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfPath)}' to '{txtFileName}'.");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch text extraction completed.");
    }
}
