using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a PDF path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        // Ensure the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor from Aspose.Pdf.Facades to extract text
        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Perform text extraction
            extractor.ExtractText();

            // Retrieve the extracted text via a stream (GetText overload requires a Stream)
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0; // reset position for reading
                using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                {
                    string extractedText = reader.ReadToEnd();
                    // Output the text to standard output
                    Console.Write(extractedText);
                }
            }
        }
    }
}
