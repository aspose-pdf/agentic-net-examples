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
            // PdfExtractor implements IDisposable, so wrap it in a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(inputPdf);

                // Extract plain text from all pages.
                extractor.ExtractText();

                // Retrieve the extracted text via the overload that writes to a Stream.
                string extractedText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream); // write text to the stream
                    textStream.Position = 0;        // rewind for reading
                    using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                    {
                        extractedText = reader.ReadToEnd();
                    }
                }

                // Write the text to a UTF‑8 encoded .txt file.
                File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
            }

            Console.WriteLine($"Text extracted to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
