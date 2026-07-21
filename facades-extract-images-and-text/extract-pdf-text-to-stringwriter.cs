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

        // Ensure the file exists before processing
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // StringWriter will hold the extracted text for logging purposes
        using (StringWriter logWriter = new StringWriter())
        {
            // PdfExtractor is a Facade class; it implements IDisposable
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // Extract text using the default Unicode encoding
                extractor.ExtractText();

                // Capture the extracted text into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the extracted text to the stream
                    extractor.GetText(ms);

                    // Reset stream position to read from the beginning
                    ms.Position = 0;

                    // Read the stream using the appropriate encoding (Unicode)
                    using (StreamReader reader = new StreamReader(ms, Encoding.Unicode))
                    {
                        // Write the extracted text into the StringWriter
                        logWriter.Write(reader.ReadToEnd());
                    }
                }
            }

            // Example: output the captured text to console (or pass to a logging framework)
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(logWriter.ToString());
        }
    }
}