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
            Console.Error.WriteLine("Usage: PdfTextExtractor <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found - {pdfPath}");
            return;
        }

        try
        {
            // Initialize the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the input PDF file
                extractor.BindPdf(pdfPath);

                // Extract text using Unicode encoding (default)
                extractor.ExtractText();

                // Retrieve the extracted text into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);

                    // Convert the byte array to a string (Unicode)
                    string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                    // Output the text to standard output
                    Console.Write(extractedText);
                }

                // Close the extractor (optional, using will dispose)
                extractor.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}