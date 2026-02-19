using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Prepare the data directory.
        // -----------------------------------------------------------------
        // Use the current working directory as a fallback if the user does not
        // provide a valid path. This makes the sample runnable on any platform
        // without requiring a hard‑coded absolute path.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"[Info] Data directory '{dataDir}' does not exist. Using current directory instead.");
            dataDir = Directory.GetCurrentDirectory();
        }

        // -----------------------------------------------------------------
        // 2. Open a password‑protected PDF document.
        // -----------------------------------------------------------------
        string encryptedPdfPath = Path.Combine(dataDir, "encrypted.pdf");
        string userPassword = "userpwd";
        string decryptedPdfPath = Path.Combine(dataDir, "decrypted.pdf");

        try
        {
            if (!File.Exists(encryptedPdfPath))
                throw new FileNotFoundException($"Encrypted PDF not found at '{encryptedPdfPath}'.");

            // Load the encrypted PDF using the constructor that accepts a password.
            Document encryptedDoc = new Document(encryptedPdfPath, userPassword);
            // Save the decrypted document.
            encryptedDoc.Save(decryptedPdfPath);
            Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Unable to process encrypted PDF: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 3. Load an MHT file and convert it to PDF.
        // -----------------------------------------------------------------
        string mhtFilePath = Path.Combine(dataDir, "sample.mht");
        string mhtPdfPath = Path.Combine(dataDir, "sample_from_mht.pdf");

        try
        {
            if (!File.Exists(mhtFilePath))
                throw new FileNotFoundException($"MHT file not found at '{mhtFilePath}'.");

            // Initialize load options for MHT files.
            MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

            // Load the MHT file using the options and convert it to PDF.
            using (Document mhtDoc = new Document(mhtFilePath, mhtLoadOptions))
            {
                mhtDoc.Save(mhtPdfPath);
                Console.WriteLine($"MHT converted PDF saved to '{mhtPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error] Unable to convert MHT to PDF: {ex.Message}");
        }
    }
}
