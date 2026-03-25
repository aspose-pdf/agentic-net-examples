using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Example URL – replace with a valid endpoint that returns a PDF.
    private const string PdfUrl = "http://example.com/sample.pdf";
    private const string UserPassword = "user123";
    private const string OwnerPassword = "owner123";

    static async Task Main(string[] args)
    {
        try
        {
            // Download the PDF into a memory stream.
            using (HttpClient httpClient = new HttpClient())
            using (HttpResponseMessage response = await httpClient.GetAsync(PdfUrl))
            {
                // Throw a clear exception if the request was not successful (e.g., 404).
                response.EnsureSuccessStatusCode();

                await using (Stream downloadStream = await response.Content.ReadAsStreamAsync())
                await using (MemoryStream pdfStream = new MemoryStream())
                {
                    // Copy the remote content into the memory stream.
                    await downloadStream.CopyToAsync(pdfStream);
                    pdfStream.Position = 0; // Reset for reading.

                    // Load the PDF from the stream without closing it when the Document is disposed.
                    using (Document doc = new Document(pdfStream, false))
                    {
                        // Define permissions (example: allow printing and content extraction).
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                        // Apply encryption using AES‑256.
                        doc.Encrypt(UserPassword, OwnerPassword, perms, CryptoAlgorithm.AESx256);

                        // Overwrite the original stream with the encrypted PDF.
                        pdfStream.SetLength(0);
                        doc.Save(pdfStream);
                        pdfStream.Position = 0; // Ready for further reading or sending back.
                    }

                    // OPTIONAL: write the encrypted PDF to a file for verification.
                    await using (FileStream file = new FileStream("encrypted.pdf", FileMode.Create, FileAccess.Write))
                    {
                        await pdfStream.CopyToAsync(file);
                    }
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.Error.WriteLine($"Failed to download PDF from '{PdfUrl}'. HTTP error: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
