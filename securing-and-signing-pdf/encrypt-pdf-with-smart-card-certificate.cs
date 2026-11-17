using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted_cert.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load a certificate from the smart card (hardware security module)
        X509Certificate2 cert = null;
        try
        {
            // Open the current user's personal store (My)
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);

                // Find the first valid certificate that has a private key
                foreach (X509Certificate2 c in store.Certificates)
                {
                    if (c.HasPrivateKey && DateTime.Now >= c.NotBefore && DateTime.Now <= c.NotAfter)
                    {
                        cert = c;
                        break;
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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define permissions for the encrypted document (use only members that exist in the current Aspose.Pdf version)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the PDF using the certificate's public key, the chosen permissions and a strong algorithm
                doc.Encrypt(perms, CryptoAlgorithm.AESx256, new List<X509Certificate2> { cert });

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted with certificate and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.Message}");
        }
    }
}
