using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "Document.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF document.
                extractor.BindPdf(inputPdfPath);

                // Extract all textual content from the document.
                extractor.ExtractText();

                // GetText does not have a parameter‑less overload. Write the text to a
                // MemoryStream, then read the stream back as a string.
                string allText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream); // writes text into the stream
                    textStream.Position = 0;        // rewind for reading
                    using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                    {
                        allText = reader.ReadToEnd();
                    }
                }

                // Write the extracted text to a plain‑text file.
                File.WriteAllText(outputTxtPath, allText);
            }

            Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
