using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Verify that the source file exists before attempting decryption.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file '{inputPath}' not found.");
            return;
        }

        // Retrieve the owner password from Azure Key Vault (placeholder implementation).
        string ownerPassword = GetOwnerPasswordFromKeyVault();
        if (string.IsNullOrEmpty(ownerPassword))
        {
            Console.Error.WriteLine("Owner password could not be retrieved.");
            return;
        }

        // Use the non‑obsolete PdfFileSecurity constructor (parameterless) and bind the PDF file
        // before calling DecryptFile. After decryption, explicitly save the result.
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            try
            {
                // Bind the encrypted PDF.
                security.BindPdf(inputPath);

                // Decrypt the PDF using the owner password.
                bool success = security.DecryptFile(ownerPassword);
                if (success)
                {
                    // Save the decrypted PDF to the desired output path.
                    security.Save(outputPath);
                    Console.WriteLine($"Decryption succeeded. Output saved to '{outputPath}'.");
                }
                else
                {
                    Console.WriteLine("Decryption failed.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during decryption: {ex.Message}");
            }
        }
    }

    // Placeholder method that simulates fetching the owner password from Azure Key Vault.
    private static string GetOwnerPasswordFromKeyVault()
    {
        // In a production scenario, use Azure.KeyVault SDK to retrieve the secret securely.
        // For this example, return a hard‑coded password.
        return "ownerpass";
    }
}
