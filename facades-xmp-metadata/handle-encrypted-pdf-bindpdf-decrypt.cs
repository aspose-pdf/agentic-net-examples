using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "output.pdf"; // retained for potential future use
        const string password = "user123";
        const string extractedTextPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Attempt to bind the PDF using a facade (PdfExtractor as an example)
        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // This will throw InvalidPasswordException if the PDF is encrypted
                extractor.BindPdf(inputPath);

                // Continue processing after successful bind
                extractor.ExtractText();
                string text = GetExtractedText(extractor);
                File.WriteAllText(extractedTextPath, text);
            }
        }
        catch (InvalidPasswordException)
        {
            Console.WriteLine("PDF is encrypted. Attempting to decrypt with provided password.");

            // Decrypt the encrypted PDF using PdfFileSecurity and work with the decrypted copy
            string tempDecryptedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // Decrypt the file
                using (PdfFileSecurity security = new PdfFileSecurity())
                {
                    security.BindPdf(inputPath);
                    // DecryptFile uses the owner password if present, otherwise the user password
                    security.DecryptFile(password);
                    security.Save(tempDecryptedPath);
                }

                // Bind the decrypted temporary file and perform the desired operation
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(tempDecryptedPath);
                    extractor.ExtractText();
                    string text = GetExtractedText(extractor);
                    File.WriteAllText(extractedTextPath, text);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to decrypt or process PDF: {ex.Message}");
            }
            finally
            {
                // Clean up the temporary decrypted file if it exists
                if (File.Exists(tempDecryptedPath))
                {
                    try { File.Delete(tempDecryptedPath); } catch { /* ignore cleanup errors */ }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error binding PDF: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves extracted text from a PdfExtractor instance.
    /// Aspose.Pdf.Facades.PdfExtractor does not provide a parameter‑less GetText() method.
    /// The correct approach is to write the text to a stream and then read it back.
    /// </summary>
    private static string GetExtractedText(PdfExtractor extractor)
    {
        using (MemoryStream textStream = new MemoryStream())
        {
            // Write extracted text into the stream
            extractor.GetText(textStream);
            // Reset position to the beginning before reading
            textStream.Position = 0;
            using (StreamReader reader = new StreamReader(textStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
