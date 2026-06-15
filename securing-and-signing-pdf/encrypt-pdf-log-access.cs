using System;
using Aspose.Pdf;

class PdfAccessLogger
{
    public void OpenDocument(string filePath, string password)
    {
        try
        {
            using (Document doc = new Document(filePath, password))
            {
                Console.WriteLine($"Successfully opened PDF with password '{password}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Access attempt failed with password '{password}': {ex.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("sample.pdf");
        }

        // Step 2: Encrypt the PDF with a user and owner password
        using (Document encryptDoc = new Document("sample.pdf"))
        {
            // No permissions (equivalent to Permissions.None in older versions)
            Permissions permissions = (Permissions)0;
            // Use a supported encryption algorithm (RC4 is obsolete in recent versions)
            CryptoAlgorithm algorithm = CryptoAlgorithm.AESx128;
            encryptDoc.Encrypt("user123", "owner123", permissions, algorithm);
            encryptDoc.Save("encrypted.pdf");
        }

        // Step 3 & 4: Log access attempts using the custom logger
        PdfAccessLogger logger = new PdfAccessLogger();
        // Attempt with an incorrect password
        logger.OpenDocument("encrypted.pdf", "wrongpass");
        // Attempt with the correct user password
        logger.OpenDocument("encrypted.pdf", "user123");
    }
}
