using System;
using System.IO;
using Aspose.Pdf;

class PdfSecurityLogger
{
    private readonly string _logFilePath;

    public PdfSecurityLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    private void Log(string message)
    {
        string entry = $"{DateTime.UtcNow:O} - {message}";
        File.AppendAllText(_logFilePath, entry + Environment.NewLine);
    }

    // Encrypt a PDF with a user and owner password
    public void EncryptPdf(string inputPath, string outputPath, string userPassword, string ownerPassword)
    {
        if (!File.Exists(inputPath))
        {
            Log($"Encrypt failed: input file not found '{inputPath}'.");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
                Log($"PDF encrypted and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Log($"Encrypt exception: {ex.Message}");
        }
    }

    // Attempt to open an encrypted PDF and log the result
    public void OpenEncryptedPdf(string encryptedPath, string password)
    {
        if (!File.Exists(encryptedPath))
        {
            Log($"Open failed: file not found '{encryptedPath}'.");
            return;
        }

        try
        {
            // Open with password; InvalidPasswordException is thrown if password is wrong
            using (Document doc = new Document(encryptedPath, password))
            {
                // Decrypt (no parameters)
                doc.Decrypt();
                Log($"Access granted for '{encryptedPath}' with provided password.");
                // Additional processing can be placed here
            }
        }
        catch (InvalidPasswordException)
        {
            Log($"Access denied for '{encryptedPath}': invalid password.");
        }
        catch (Exception ex)
        {
            Log($"Error opening '{encryptedPath}': {ex.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string encryptedPdf = "sample_encrypted.pdf";
        const string userPwd = "user123";
        const string ownerPwd = "owner123";
        const string logPath = "access_log.txt";

        PdfSecurityLogger logger = new PdfSecurityLogger(logPath);

        // Encrypt the PDF (run once)
        logger.EncryptPdf(inputPdf, encryptedPdf, userPwd, ownerPwd);

        // Simulate access attempts
        logger.OpenEncryptedPdf(encryptedPdf, "wrongPassword"); // should log denial
        logger.OpenEncryptedPdf(encryptedPdf, userPwd);         // should log success
    }
}