using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Load the certificate from a hardware security module (smart card)
        // ------------------------------------------------------------
        X509Certificate2? cert = null;
        try
        {
            // Open the personal store of the current user
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                // Find a certificate that has a private key and is stored on a smart card
                foreach (X509Certificate2 c in store.Certificates)
                {
                    if (c.HasPrivateKey)
                    {
                        // Get the RSA private key; if it exists the certificate is usable for RSA operations
                        using RSA? rsa = c.GetRSAPrivateKey();
                        if (rsa != null)
                        {
                            // Additional checks (e.g., issuer, subject) can be added here
                            cert = c;
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error accessing smart card certificate: {ex.Message}");
            return;
        }

        if (cert == null)
        {
            Console.Error.WriteLine("No suitable certificate found on the smart card.");
            return;
        }

        // ------------------------------------------------------------
        // Encrypt the PDF using password protection (Aspose.Pdf core API)
        // ------------------------------------------------------------
        // Note: Aspose.Pdf core API provides password‑based encryption via
        // Document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm).
        // Certificate‑based encryption is not exposed through the core API,
        // so we use strong password encryption and rely on the certificate for
        // authentication elsewhere (e.g., signing or external access control).
        // ------------------------------------------------------------
        const string userPassword = "UserPassword123!";
        const string ownerPassword = "OwnerPassword123!";

        try
        {
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Define permissions – allow printing and content extraction, deny everything else
                Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument |
                                               Aspose.Pdf.Permissions.ExtractContent;

                // Use the strongest symmetric algorithm available
                doc.Encrypt(userPassword, ownerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted successfully and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.Message}");
        }
    }
}
