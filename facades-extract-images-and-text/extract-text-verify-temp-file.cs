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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (facade) to extract text from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Perform the extraction (Unicode encoding is default)
            extractor.ExtractText();

            // ---- Get extracted text into memory ----
            string extractedText;
            using (MemoryStream memStream = new MemoryStream())
            {
                // Save extracted text to the memory stream
                extractor.GetText(memStream);

                // Convert the stream bytes to a string (Unicode)
                extractedText = Encoding.Unicode.GetString(memStream.ToArray());
            }

            // ---- Save extracted text to a temporary file ----
            string tempFilePath = Path.GetTempFileName();
            try
            {
                // Write the same extracted text to the temp file using the facade
                extractor.GetText(tempFilePath);

                // ---- Read the text back from the temporary file ----
                string readBackText = File.ReadAllText(tempFilePath, Encoding.Unicode);

                // ---- Verify that the two strings are identical ----
                if (extractedText == readBackText)
                {
                    Console.WriteLine("Verification succeeded: extracted content matches the file content.");
                }
                else
                {
                    Console.WriteLine("Verification failed: extracted content does NOT match the file content.");
                }
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}