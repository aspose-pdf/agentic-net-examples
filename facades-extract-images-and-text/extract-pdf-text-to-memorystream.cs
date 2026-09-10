using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // MemoryStream that will hold the extracted text
        using (MemoryStream textStream = new MemoryStream())
        {
            // Use PdfExtractor (facade) to extract text from the PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // Extract text using Unicode encoding (default)
                extractor.ExtractText();

                // Save the extracted text into the MemoryStream
                extractor.GetText(textStream);
            }

            // Reset stream position for reading by the next component
            textStream.Position = 0;

            // Example: read the text as a string (optional, for verification)
            string extractedText = new StreamReader(textStream, Encoding.Unicode).ReadToEnd();
            Console.WriteLine("Extracted text length: " + extractedText.Length);

            // Reset again before passing to another library
            textStream.Position = 0;

            // ------------------------------------------------------------
            // Pass the MemoryStream (textStream) to another library that
            // generates a PDF from the extracted text.
            // The receiving library should read from the provided stream.
            // Example placeholder:
            // OtherPdfGenerator.GeneratePdfFromTextStream(textStream, "output.pdf");
            // ------------------------------------------------------------
        }
    }
}