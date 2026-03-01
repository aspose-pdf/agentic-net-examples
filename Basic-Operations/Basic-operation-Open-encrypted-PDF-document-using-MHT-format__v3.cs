using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // For PdfFileSecurity if needed

class Program
{
    static void Main()
    {
        // Paths and password
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword      = "user123";
        const string outputHtmlPath    = "output.html";

        // Verify input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password.
            // Document(string, string) constructor handles password‑protected PDFs.
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Optional: decrypt the document in memory (not required for reading).
                // doc.Decrypt();

                // Save the PDF as HTML (MHT‑like) using HtmlSaveOptions.
                // HtmlSaveOptions is in the Aspose.Pdf namespace.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (images, fonts, CSS) into a single HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                doc.Save(outputHtmlPath, htmlOpts);
            }

            Console.WriteLine($"Decrypted PDF saved as HTML to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}