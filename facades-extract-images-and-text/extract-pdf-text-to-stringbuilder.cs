using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output text file path (after manipulation)
        const string outputTxt = "extracted.txt";

        // StringBuilder to hold extracted text for further processing
        StringBuilder sb = new StringBuilder();

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (Facade) to extract text from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Extract text using Unicode encoding (default)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Save extracted text to the stream
                extractor.GetText(ms);

                // Reset stream position before reading
                ms.Position = 0;

                // Convert the stream bytes to a string (Unicode)
                string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                // Append the text to the StringBuilder for further manipulation
                sb.Append(extractedText);
            }
        }

        // Example manipulation: trim leading/trailing whitespace
        string processedText = sb.ToString().Trim();

        // Write the processed text to disk
        File.WriteAllText(outputTxt, processedText, Encoding.Unicode);

        Console.WriteLine($"Text extracted and saved to '{outputTxt}'.");
    }
}