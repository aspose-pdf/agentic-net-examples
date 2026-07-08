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

        // Extract text from the PDF using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // Load the PDF
            extractor.ExtractText();             // Perform Unicode text extraction

            // Capture the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);           // Write text bytes to the stream
                ms.Position = 0;                 // Reset for reading

                // Read the stream using Unicode encoding
                using (StreamReader reader = new StreamReader(ms, Encoding.Unicode))
                {
                    string extracted = reader.ReadToEnd();

                    // Write the text to a StringWriter (suitable for logging frameworks)
                    using (StringWriter stringWriter = new StringWriter())
                    {
                        stringWriter.Write(extracted);

                        // Example: output to console (replace with logger as needed)
                        Console.WriteLine(stringWriter.ToString());
                    }
                }
            }
        }
    }
}