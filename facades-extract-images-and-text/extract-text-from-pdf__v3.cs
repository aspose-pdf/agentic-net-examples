using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: PdfTextExtract <pdfPath>");
            return;
        }

        string pdfPath = args[0];

        // Ensure the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor (Facade) to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Retrieve extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                // Convert the stream bytes to a string (Unicode)
                string extractedText = Encoding.Unicode.GetString(ms.ToArray());
                // Output the text to standard output
                Console.Write(extractedText);
            }

            // Optional explicit close (Dispose will be called by using)
            extractor.Close();
        }
    }
}