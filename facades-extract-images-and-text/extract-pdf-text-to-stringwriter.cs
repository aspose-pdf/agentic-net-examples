using System;
using System.IO;
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

        // Use PdfExtractor facade to extract text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // Load the PDF
            extractor.ExtractText();             // Perform extraction (Unicode by default)

            // Capture extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);           // Write text to the stream
                ms.Position = 0;                 // Reset for reading

                // Read the stream into a string
                using (StreamReader reader = new StreamReader(ms))
                {
                    string extractedText = reader.ReadToEnd();

                    // Write the text to a StringWriter (suitable for logging frameworks)
                    using (StringWriter stringWriter = new StringWriter())
                    {
                        stringWriter.Write(extractedText);

                        // Example output – replace with actual logging as needed
                        Console.WriteLine(stringWriter.ToString());
                    }
                }
            }
        }
    }
}