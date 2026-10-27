using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        string inputPdf = "input.pdf";
        string signedPdf = "signed_output.pdf";

        // Retrieve the certificate (PFX) as a byte array.
        // In a real scenario you would obtain this from Azure Key Vault.
        // For this self‑contained example we read a Base64‑encoded string from an environment variable.
        byte[] pfxBytes = GetCertificateBytes();
        if (pfxBytes == null || pfxBytes.Length == 0)
        {
            Console.WriteLine("Certificate data not found. Set the CERT_BASE64 environment variable with a Base64‑encoded PFX.");
            return;
        }

        using var pfxStream = new MemoryStream(pfxBytes);

        // Load the PDF document
        var doc = new Document(inputPdf);

        // Define the rectangle where the visible signature will appear (coordinates are in points)
        var sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

        // Create a signature field on the first page and add it to the form
        var signatureField = new SignatureField(doc.Pages[1], sigRect)
        {
            PartialName = "Signature1"
        };
        doc.Form.Add(signatureField, 1);

        // Reset the stream position before it is used a second time
        pfxStream.Position = 0;

        // Create a PKCS7 signature object using the certificate stream (no password in this example)
        var pkcs7 = new PKCS7(pfxStream, (string)null)
        {
            Reason = "Document signed with Azure Key Vault certificate",
            Location = "Location",
            ContactInfo = "Contact Info"
        };

        // Reset the stream again because PKCS7 constructor consumes it
        pfxStream.Position = 0;

        // Sign the field with the PKCS7 object and the certificate stream
        signatureField.Sign(pkcs7, pfxStream, (string)null);

        // Save the signed PDF
        doc.Save(signedPdf);

        Console.WriteLine($"Signed PDF saved to '{signedPdf}'.");
    }

    /// <summary>
    /// Retrieves the PFX certificate bytes.
    /// This implementation reads a Base64‑encoded string from the environment variable "CERT_BASE64".
    /// In production replace this with Azure Key Vault retrieval logic.
    /// </summary>
    private static byte[] GetCertificateBytes()
    {
        string base64 = Environment.GetEnvironmentVariable("CERT_BASE64");
        if (string.IsNullOrWhiteSpace(base64))
            return null;
        try
        {
            return Convert.FromBase64String(base64);
        }
        catch
        {
            Console.WriteLine("Failed to decode CERT_BASE64 environment variable.");
            return null;
        }
    }
}
