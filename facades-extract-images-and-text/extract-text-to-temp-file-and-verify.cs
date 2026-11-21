using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Extract text from PDF using PdfExtractor (Facades API)
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);
                extractor.ExtractText();

                // Get the extracted text via the overload that writes to a Stream
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

                // Save extracted text to a temporary .txt file
                string tempFilePath = Path.Combine(Path.GetTempPath(),
                                                   Path.GetRandomFileName() + ".txt");
                File.WriteAllText(tempFilePath, extractedText);

                // Read the text back from the temporary file
                string readBackText = File.ReadAllText(tempFilePath);

                // Verify that the read text matches the originally extracted text
                if (extractedText == readBackText)
                {
                    Console.WriteLine("Verification succeeded: extracted text matches the saved content.");
                }
                else
                {
                    Console.WriteLine("Verification failed: mismatch between extracted and saved text.");
                }

                // Optional cleanup of the temporary file
                try { File.Delete(tempFilePath); } catch { /* ignore cleanup errors */ }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
