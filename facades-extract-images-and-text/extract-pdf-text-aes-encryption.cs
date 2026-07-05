using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added namespace for TextFragment

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // Source PDF (will be created if missing)
        const string txtPath = "extracted.txt";             // Temporary plain‑text file
        const string encryptedPath = "extracted_encrypted.bin"; // AES‑encrypted output

        // ------------------------------------------------------------
        // Ensure a PDF exists – create a minimal one if the file is absent.
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            // Create a simple PDF with a single page containing some text.
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("This is sample text that will be extracted and encrypted."));
                doc.Save(pdfPath);
            }
        }

        // ------------------------------------------------------------
        // Extract text from PDF using PdfExtractor (Aspose.Pdf.Facades).
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);                     // Bind the PDF file
            extractor.ExtractText();                        // Extract text (default Unicode)
            extractor.GetText(txtPath);                     // Save extracted text to a .txt file
        }

        // ------------------------------------------------------------
        // Read the extracted plain‑text.
        // ------------------------------------------------------------
        byte[] plainBytes = File.ReadAllBytes(txtPath);

        // ------------------------------------------------------------
        // Encrypt the text with AES‑256.
        // ------------------------------------------------------------
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();

            // Write IV + Key + Ciphertext to the output file.
            // NOTE: In production the key should be stored securely, not concatenated.
            using (FileStream outStream = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write))
            {
                outStream.Write(aes.IV, 0, aes.IV.Length);   // 16‑byte IV
                outStream.Write(aes.Key, 0, aes.Key.Length); // 32‑byte key

                using (CryptoStream cryptoStream = new CryptoStream(outStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary plain‑text file.
        // ------------------------------------------------------------
        if (File.Exists(txtPath))
        {
            File.Delete(txtPath);
        }

        Console.WriteLine($"Extraction and encryption completed. Encrypted file: {encryptedPath}");
    }
}
