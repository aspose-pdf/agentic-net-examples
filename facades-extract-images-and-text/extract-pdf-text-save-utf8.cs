using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Create the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file
                extractor.BindPdf(inputPdfPath);

                // Extract text using Unicode encoding to preserve international characters
                extractor.ExtractText(Encoding.Unicode);

                // Retrieve the extracted text into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);

                    // Convert the byte array to a UTF‑8 string
                    string extractedText = Encoding.UTF8.GetString(ms.ToArray());

                    // Save the text to a file with UTF‑8 encoding
                    File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
                }
            }

            Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}