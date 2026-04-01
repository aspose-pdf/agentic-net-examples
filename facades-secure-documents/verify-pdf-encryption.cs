using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        string inputDir = "pdfs";
        Directory.CreateDirectory(inputDir);

        // Create sample PDFs for demonstration
        // Unencrypted PDF
        using (Document plainDoc = new Document())
        {
            plainDoc.Pages.Add();
            plainDoc.Save(Path.Combine(inputDir, "plain.pdf"));
        }
        // Encrypted PDF (AES‑256)
        using (Document encDoc = new Document())
        {
            encDoc.Pages.Add();
            encDoc.Encrypt("user", "owner", Permissions.PrintDocument, CryptoAlgorithm.AESx256);
            encDoc.Save(Path.Combine(inputDir, "encrypted.pdf"));
        }

        Console.WriteLine("Encryption Report:");
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            try
            {
                // If the document opens without a password, it is not encrypted
                using (Document doc = new Document(pdfPath))
                {
                    Console.WriteLine($"{fileName}: Not encrypted");
                }
            }
            catch (InvalidPasswordException)
            {
                // Document is encrypted – attempt to read the algorithm (if supported)
                try
                {
                    PdfFileSecurity security = new PdfFileSecurity();
                    security.BindPdf(pdfPath);
                    // NOTE: Aspose.Pdf does not expose the encryption algorithm directly.
                    // The following line is a placeholder for the actual property/method
                    // that would return the CryptoAlgorithm used. Replace with the correct API
                    // when available, e.g., CryptoAlgorithm algo = security.EncryptionAlgorithm;
                    Console.WriteLine($"{fileName}: Encrypted (algorithm unknown)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{fileName}: Encrypted (unable to determine algorithm) – {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{fileName}: Error – {ex.Message}");
            }
        }
    }
}
