using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        string mhtFile = Path.Combine(dataDir, "sample.mht");          // MHT source
        string pdfFromMht = Path.Combine(dataDir, "sample_from_mht.pdf"); // Output PDF
        string encryptedPdf = Path.Combine(dataDir, "encrypted.pdf"); // Encrypted PDF source
        string decryptedPdf = Path.Combine(dataDir, "decrypted.pdf"); // Output after opening

        // Password for the encrypted PDF (user password)
        const string userPassword = "userpwd";

        try
        {
            // -----------------------------------------------------------------
            // 1. Load an MHT file and convert it to PDF
            // -----------------------------------------------------------------
            if (!File.Exists(mhtFile))
                throw new FileNotFoundException($"MHT file not found: {mhtFile}");

            // Initialize load options for MHT
            MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

            // Load the MHT file into a PDF document
            using (Document pdfDoc = new Document(mhtFile, mhtLoadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(pdfFromMht);
            }

            // -----------------------------------------------------------------
            // 2. Open an encrypted PDF document using its password
            // -----------------------------------------------------------------
            if (!File.Exists(encryptedPdf))
                throw new FileNotFoundException($"Encrypted PDF not found: {encryptedPdf}");

            // The Document constructor that accepts (string filePath, string password)
            // opens the PDF with the supplied user password.
            using (Document encryptedDoc = new Document(encryptedPdf, userPassword))
            {
                // Save a copy of the decrypted document
                encryptedDoc.Save(decryptedPdf);
            }

            Console.WriteLine("Operations completed successfully.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("The provided password is incorrect for the encrypted PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}