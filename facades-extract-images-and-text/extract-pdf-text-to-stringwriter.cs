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

        // StringWriter will hold the extracted text for logging
        using (StringWriter logWriter = new StringWriter())
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // Extract text using Unicode encoding
                extractor.ExtractText(Encoding.Unicode);

                // Capture the extracted text into a MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);
                    ms.Position = 0; // Reset stream position for reading

                    // Read the stream as a Unicode string
                    using (StreamReader reader = new StreamReader(ms, Encoding.Unicode))
                    {
                        string extractedText = reader.ReadToEnd();
                        logWriter.Write(extractedText);
                    }
                }
            }

            // Example output: write the captured text to the console (or pass to a logging framework)
            Console.WriteLine(logWriter.ToString());
        }
    }
}