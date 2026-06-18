using System;
using System.IO;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure SDK types when the real packages are not referenced.
// This allows the sample to compile and run in environments where the Azure
// libraries are unavailable. In production you should reference the official
// NuGet packages:
//   Azure.Identity
//   Azure.Security.KeyVault.Secrets
// and remove these stub definitions.
// ---------------------------------------------------------------------------
namespace Azure.Identity
{
    // Represents the default Azure credential chain. The stub does nothing.
    public class DefaultAzureCredential { }
}

namespace Azure.Security.KeyVault.Secrets
{
    /// <summary>
    /// Simple representation of a secret stored in Azure Key Vault.
    /// </summary>
    public class KeyVaultSecret
    {
        public string Value { get; }
        public KeyVaultSecret(string value) => Value = value;
    }

    /// <summary>
    /// Very small stub that mimics the Azure SDK SecretClient. It retrieves the
    /// secret value from an environment variable whose name matches the secret
    /// name. This is sufficient for demonstration and unit‑testing purposes.
    /// </summary>
    public class SecretClient
    {
        private readonly Uri _vaultUri;
        private readonly Azure.Identity.DefaultAzureCredential _credential;

        public SecretClient(Uri vaultUri, Azure.Identity.DefaultAzureCredential credential)
        {
            _vaultUri = vaultUri ?? throw new ArgumentNullException(nameof(vaultUri));
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <summary>
        /// Retrieves a secret. In the stub implementation the secret value is read
        /// from an environment variable named exactly as <paramref name="name"/>.
        /// </summary>
        public KeyVaultSecret GetSecret(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Secret name cannot be null or whitespace.", nameof(name));

            // In a real implementation the secret would be fetched from Key Vault.
            // Here we fall back to an environment variable for simplicity.
            string envValue = Environment.GetEnvironmentVariable(name);
            if (envValue == null)
                throw new InvalidOperationException($"Secret '{name}' not found in environment variables.");

            return new KeyVaultSecret(envValue);
        }
    }
}

class Program
{
    static void Main()
    {
        // Paths to the encrypted input PDF and the decrypted output PDF.
        const string inputPdfPath = "encrypted_input.pdf";
        const string outputPdfPath = "decrypted_output.pdf";

        // Azure Key Vault configuration.
        // KV_URL example: "https://myvault.vault.azure.net/"
        // SECRET_NAME is the name of the secret that stores the owner password.
        string keyVaultUrl = Environment.GetEnvironmentVariable("KV_URL");
        string secretName = Environment.GetEnvironmentVariable("KV_SECRET_NAME");

        if (string.IsNullOrWhiteSpace(keyVaultUrl) || string.IsNullOrWhiteSpace(secretName))
        {
            Console.Error.WriteLine("Key Vault URL or secret name not configured in environment variables.");
            return;
        }

        try
        {
            // Retrieve the owner password from Azure Key Vault (or stub).
            var client = new Azure.Security.KeyVault.Secrets.SecretClient(
                new Uri(keyVaultUrl),
                new Azure.Identity.DefaultAzureCredential()
            );
            Azure.Security.KeyVault.Secrets.KeyVaultSecret secret = client.GetSecret(secretName);
            string ownerPassword = secret.Value;

            // Ensure the input file exists.
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
                return;
            }

            // Decrypt the PDF using Aspose.Pdf.Facades.PdfFileSecurity.
            using (var fileSecurity = new PdfFileSecurity())
            {
                // Bind the encrypted PDF.
                fileSecurity.BindPdf(inputPdfPath);

                // Decrypt with the owner password retrieved from Key Vault.
                bool success = fileSecurity.DecryptFile(ownerPassword);
                if (!success)
                {
                    Console.Error.WriteLine("Decryption failed. Verify that the owner password is correct.");
                    return;
                }

                // Save the decrypted PDF.
                fileSecurity.Save(outputPdfPath);
            }

            Console.WriteLine($"Decryption completed. Decrypted file saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
