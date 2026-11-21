using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certThumbprint = "YOUR_CERT_THUMBPRINT"; // replace with the actual thumbprint of the smart‑card certificate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Locate the certificate stored on the smart card (CurrentUser\My store)
        X509Certificate2 cert = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (var c in store.Certificates)
            {
                if (string.Equals(c.Thumbprint, certThumbprint, StringComparison.OrdinalIgnoreCase) && c.HasPrivateKey)
                {
                    cert = c;
                    break;
                }
            }
        }

        if (cert == null)
        {
            Console.Error.WriteLine("Certificate not found on the smart card or it does not contain a private key.");
            return;
        }

        try
        {
            // Load the PDF document
            var doc = new Document(inputPath);

            // Create a visible signature field on the first page (you can set visible = false later)
            var rect = new Rectangle(100, 100, 200, 150); // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            var sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "SmartCardSignature"
            };
            doc.Form.Add(sigField, 1);

            // Build an ExternalSignature that uses the smart‑card certificate directly.
            var externalSig = new ExternalSignature(cert)
            {
                Reason = "Approved",
                Location = "Office",
                ContactInfo = string.Empty,
                CustomAppearance = new SignatureCustomAppearance
                {
                    FontSize = 8,
                    ShowContactInfo = false,
                    ShowLocation = false,
                    ShowReason = true,
                    BackgroundColor = Color.FromArgb(80, 173, 216, 230) // optional semi‑transparent background
                }
            };

            // Sign the field. The signature is written into the document.
            sigField.Sign(externalSig);

            // Save the signed PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF signed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Signing failed: {ex.Message}");
        }
    }
}
