using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the PDF file
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: PdfTextExtract <pdf-file-path>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found - {pdfPath}");
            return;
        }

        // Use PdfExtractor (a Facade) to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract text using Unicode encoding (default mode)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);

                // Convert the stream bytes to a string (Unicode)
                string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                // Output the text to standard output
                Console.Write(extractedText);
            }
        }
    }
}