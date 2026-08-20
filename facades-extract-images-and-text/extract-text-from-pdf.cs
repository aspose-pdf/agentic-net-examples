using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the PDF file
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <executable> <pdfPath>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor from Aspose.Pdf.Facades to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                // Output the extracted text to the console
                Console.WriteLine(extractedText);
            }
        }
    }
}