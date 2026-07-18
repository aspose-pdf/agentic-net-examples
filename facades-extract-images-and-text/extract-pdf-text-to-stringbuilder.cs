using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // StringBuilder will hold the extracted text for further manipulation
        StringBuilder sb = new StringBuilder();

        // PdfExtractor is a Facade class that implements IDisposable
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPath);

            // Perform text extraction (Unicode encoding is the default)
            extractor.ExtractText();

            // Iterate through each page's extracted text
            while (extractor.HasNextPageText())
            {
                // Use a memory stream to capture the page text
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetNextPageText(ms);

                    // Convert the Unicode bytes to a string
                    string pageText = Encoding.Unicode.GetString(ms.ToArray());

                    // Append the page text to the StringBuilder
                    sb.AppendLine(pageText);
                }
            }
        }

        // The StringBuilder now contains the full document text
        Console.WriteLine("Extracted text:");
        Console.WriteLine(sb.ToString());

        // Optionally, write the accumulated text to a file
        const string outputPath = "extracted.txt";
        File.WriteAllText(outputPath, sb.ToString(), Encoding.Unicode);
        Console.WriteLine($"Text saved to {outputPath}");
    }
}