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

        // StringBuilder to hold the extracted text for further manipulation
        StringBuilder extractedTextBuilder = new StringBuilder();

        // Ensure the PDF file exists before processing
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor (Facade) to extract text from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Perform text extraction using Unicode encoding (default)
            extractor.ExtractText();

            // Save the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);

                // Convert the memory stream bytes to a string (Unicode)
                string pageText = Encoding.Unicode.GetString(ms.ToArray());

                // Append the extracted text to the StringBuilder
                extractedTextBuilder.Append(pageText);
            }
        }

        // At this point, extractedTextBuilder contains the full PDF text.
        // Example: further manipulation or writing to disk
        Console.WriteLine("Extracted Text:");
        Console.WriteLine(extractedTextBuilder.ToString());

        // Optionally, write the text to a file
        // File.WriteAllText("output.txt", extractedTextBuilder.ToString(), Encoding.Unicode);
    }
}