using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

public class PdfEncryptionExample
{
    // Reads a PDF from a URL, encrypts it, and writes the secured PDF to the provided stream.
    public static async Task EncryptPdfFromUrlAsync(string pdfUrl, string userPassword, string ownerPassword, Stream outputStream)
    {
        // Download the PDF into a memory buffer.
        using (HttpClient client = new HttpClient())
        {
            // Get the response and ensure the request succeeded (e.g., 200 OK).
            using (HttpResponseMessage response = await client.GetAsync(pdfUrl))
            {
                if (!response.IsSuccessStatusCode)
                {
                    // Throw a more descriptive exception so callers know why it failed.
                    throw new HttpRequestException($"Failed to download PDF. Status code: {response.StatusCode}");
                }

                using (Stream downloadStream = await response.Content.ReadAsStreamAsync())
                using (MemoryStream inputMemory = new MemoryStream())
                {
                    await downloadStream.CopyToAsync(inputMemory);
                    inputMemory.Position = 0; // Reset for reading.

                    // Load the PDF from the memory stream.
                    using (Document doc = new Document(inputMemory))
                    {
                        // Define permissions (example: allow printing and content extraction).
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                        // Apply encryption with AES‑256.
                        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                        // Save the encrypted PDF to the output stream.
                        doc.Save(outputStream);

                        // Rewind the stream so callers can read from the beginning.
                        if (outputStream.CanSeek)
                        {
                            outputStream.Position = 0;
                        }
                    }
                }
            }
        }
    }

    // Sample console entry point demonstrating usage.
    public static async Task Main()
    {
        string pdfUrl   = "https://example.com/sample.pdf";
        string userPwd  = "user123";
        string ownerPwd = "owner123";

        using (MemoryStream encryptedPdf = new MemoryStream())
        {
            try
            {
                await EncryptPdfFromUrlAsync(pdfUrl, userPwd, ownerPwd, encryptedPdf);

                // For demonstration, write the encrypted PDF to a file.
                File.WriteAllBytes("secured.pdf", encryptedPdf.ToArray());
                Console.WriteLine("Encrypted PDF saved as secured.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
