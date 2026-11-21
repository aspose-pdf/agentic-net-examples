using System;
using System.IO;
using Aspose.Pdf;               // for PdfException
using Aspose.Pdf.Facades;      // for PdfExtractor

class Program
{
    static void Main()
    {
        const string inputPath  = "corrupted.pdf";
        const string outputPath = "extracted.txt";

        // Verify the input file exists before attempting to bind it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // BindPdf may throw if the PDF is corrupted or unreadable
                extractor.BindPdf(inputPath);

                // Extract all text from the PDF
                extractor.ExtractText();

                // Get the extracted text via the overload that writes to a Stream
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream); // write text to the stream
                    textStream.Position = 0;        // rewind for reading
                    using (StreamReader reader = new StreamReader(textStream))
                    {
                        string extractedText = reader.ReadToEnd();
                        // Write extracted text to a file
                        File.WriteAllText(outputPath, extractedText);
                    }
                }

                Console.WriteLine($"Text successfully extracted to '{outputPath}'.");
            }
        }
        // Catch the specific Aspose.Pdf exception for binding failures
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"PdfExtractor failed to bind the PDF: {pdfEx.Message}");
        }
        // Catch any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
