using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable; wrap it in a using block for deterministic disposal
            using (var extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);

                // Extract all text using UTF‑8 encoding. This method does NOT write to a file.
                extractor.ExtractText(Encoding.UTF8);

                // Retrieve the extracted text into a memory stream, then write it to a .txt file.
                using (var ms = new MemoryStream())
                {
                    extractor.GetText(ms); // writes the text into the stream
                    string text = Encoding.UTF8.GetString(ms.ToArray());
                    File.WriteAllText(outputTxt, text, Encoding.UTF8);
                }
            }

            Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Extraction failed: {ex.Message}");
        }
    }
}
