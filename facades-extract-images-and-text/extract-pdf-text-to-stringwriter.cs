using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor (facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract all text using Unicode encoding (default)
            extractor.ExtractText();

            // Capture the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Save extracted text to the stream
                extractor.GetText(ms);

                // Convert the stream bytes to a string (Unicode)
                string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                // Write the text to a StringWriter (useful for logging frameworks)
                using (StringWriter stringWriter = new StringWriter())
                {
                    stringWriter.Write(extractedText);

                    // Example: output the captured text (replace with actual logging)
                    Console.WriteLine(stringWriter.ToString());
                }
            }
        }
    }
}