using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Ensure the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Extract text from the PDF into a memory stream
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            // MemoryStream will hold the extracted text
            using (MemoryStream textStream = new MemoryStream())
            {
                // Save the extracted text into the stream
                extractor.GetText(textStream);

                // Reset stream position for reading by the next component
                textStream.Position = 0;

                // ------------------------------------------------------------
                // Pass the memory stream to another library that generates a PDF.
                // The following is a placeholder for that library's API.
                // Replace 'OtherPdfGenerator' and its method with the actual
                // implementation you intend to use.
                // ------------------------------------------------------------
                // Example:
                // OtherPdfGenerator.CreatePdfFromTextStream(textStream, "output_generated.pdf");
                // ------------------------------------------------------------

                // For demonstration, we simply read the text and display it.
                using (StreamReader reader = new StreamReader(textStream, Encoding.Unicode))
                {
                    string extractedText = reader.ReadToEnd();
                    Console.WriteLine("Extracted Text:");
                    Console.WriteLine(extractedText);
                }
            }
        }
    }
}