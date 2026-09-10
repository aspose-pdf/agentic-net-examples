using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // StringBuilder to hold the extracted text for further manipulation
        StringBuilder extractedTextBuilder = new StringBuilder();

        // Use PdfExtractor (Facade) to bind the PDF and extract its text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdfPath);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Save the extracted text into a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // Reset stream position before reading
                textStream.Position = 0;

                // Convert the stream bytes to a string (Unicode encoding)
                string pageText = Encoding.Unicode.GetString(textStream.ToArray());

                // Append the text to the StringBuilder
                extractedTextBuilder.Append(pageText);
            }
        }

        // At this point you can manipulate the text in the StringBuilder as needed
        // For demonstration, we simply write it to a file
        File.WriteAllText(outputTxtPath, extractedTextBuilder.ToString(), Encoding.Unicode);

        Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
    }
}