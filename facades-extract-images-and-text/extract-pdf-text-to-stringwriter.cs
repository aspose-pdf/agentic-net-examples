using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // StringWriter will collect the extracted text for logging or further processing
        using (StringWriter logWriter = new StringWriter())
        {
            ExtractPdfTextToWriter(inputPdf, logWriter);

            // Example: output the collected text to console (replace with actual logging)
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(logWriter.ToString());
        }
    }

    /// <summary>
    /// Extracts all text from a PDF file using Aspose.Pdf.Facades.PdfExtractor
    /// and writes it into the provided TextWriter (e.g., StringWriter).
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="writer">TextWriter that will receive the extracted text.</param>
    static void ExtractPdfTextToWriter(string pdfPath, TextWriter writer)
    {
        // Initialize the extractor and bind the PDF document
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Capture the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Save extracted text to the memory stream
                extractor.GetText(ms);

                // Reset stream position to read from the beginning
                ms.Position = 0;

                // Read the stream using the appropriate encoding (Unicode)
                using (StreamReader sr = new StreamReader(ms, Encoding.Unicode))
                {
                    string extracted = sr.ReadToEnd();
                    writer.Write(extracted);
                }
            }
        }
    }
}