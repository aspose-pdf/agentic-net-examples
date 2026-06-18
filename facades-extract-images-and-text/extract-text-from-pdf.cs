using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the PDF file.
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: ExtractText <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{pdfPath}'");
            return;
        }

        try
        {
            // PdfExtractor implements Facade (which provides Dispose), so we use a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);

                // Perform the text extraction.
                extractor.ExtractText();

                // Retrieve the extracted text into a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);

                    // Convert the stream bytes to a string (Unicode encoding is used by default).
                    string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                    // Output the extracted text to the console.
                    Console.WriteLine(extractedText);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}