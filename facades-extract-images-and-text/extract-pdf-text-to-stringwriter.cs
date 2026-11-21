using System;
using System.IO;
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

        try
        {
            // Bind the PDF to the extractor and extract all text
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPath);
                extractor.ExtractText();

                // Get the extracted text via a stream (required overload)
                string extractedText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream); // write text to the stream
                    textStream.Position = 0;        // rewind for reading
                    using (StreamReader reader = new StreamReader(textStream))
                    {
                        extractedText = reader.ReadToEnd();
                    }
                }

                // Write the text to a StringWriter (suitable for logging frameworks)
                using (StringWriter stringWriter = new StringWriter())
                {
                    stringWriter.Write(extractedText);

                    // Example output – replace with your logger as needed
                    Console.WriteLine(stringWriter.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting text: {ex.Message}");
        }
    }
}
