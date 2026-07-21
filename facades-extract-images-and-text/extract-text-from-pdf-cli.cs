using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly one argument: the path to the PDF file.
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: PdfTextExtractor <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{pdfPath}'.");
            return;
        }

        try
        {
            // Initialize the PdfExtractor facade.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);

                // Extract text using Unicode encoding (default).
                extractor.ExtractText();

                // Retrieve the extracted text into a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);

                    // Convert the stream contents to a string.
                    string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                    // Output the text to standard output.
                    Console.Write(extractedText);
                }

                // Close the extractor (optional, Dispose will handle it).
                extractor.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}