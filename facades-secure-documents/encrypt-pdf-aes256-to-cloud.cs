using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfEncryptionUtility
{
    /// <summary>
    /// Encrypts a PDF using AES‑256 and writes the result to the specified destination (e.g., a cloud storage bucket).
    /// </summary>
    /// <param name="inputFile">Path to the source PDF.</param>
    /// <param name="outputFile">Path where the encrypted PDF will be saved (cloud bucket URI or local path).</param>
    /// <param name="userPassword">User password (can be empty for no user password).</param>
    /// <param name="ownerPassword">Owner password (can be empty; a random one will be generated).</param>
    public static void EncryptPdf(string inputFile, string outputFile, string userPassword, string ownerPassword)
    {
        // Verify that the source file exists.
        if (!File.Exists(inputFile))
        {
            Console.Error.WriteLine($"Input file not found: {inputFile}");
            return;
        }

        // PdfFileSecurity implements IDisposable, so wrap it in a using block.
        using (PdfFileSecurity security = new PdfFileSecurity(inputFile, outputFile))
        {
            // Define the desired privileges for the encrypted document.
            // Here we allow printing; adjust as needed.
            DocumentPrivilege privilege = DocumentPrivilege.Print;

            // Encrypt using a 256‑bit key with the AES algorithm.
            // The method returns true on success.
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                privilege,
                KeySize.x256,
                Algorithm.AES);

            if (!encrypted)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        // The encrypted PDF is now saved at the output location.
        Console.WriteLine($"Encrypted PDF saved to: {outputFile}");
    }

    static void Main()
    {
        // Example usage:
        string sourcePdf = "input.pdf";                     // Local source PDF.
        string destination = "gs://my-bucket/encrypted.pdf"; // Destination URI (cloud bucket) or local path.
        string userPwd = "user123";
        string ownerPwd = "owner123";

        EncryptPdf(sourcePdf, destination, userPwd, ownerPwd);
    }
}