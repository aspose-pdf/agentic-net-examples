using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – in a real scenario the PFX would be obtained from Azure Key Vault.
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string pfxPath       = "signer.pfx";          // local placeholder for the KV secret
        const string pfxPassword   = "password";           // placeholder password

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the certificate – this also validates that the PFX is correct.
        X509Certificate2 cert;
        try
        {
            cert = new X509Certificate2(pfxPath, pfxPassword,
                X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load certificate: {ex.Message}");
            return;
        }

        // Load the PDF into an expandable memory stream (required for core signing).
        byte[] pdfBytes = File.ReadAllBytes(inputPdfPath);
        using var sourceStream = new MemoryStream();
        sourceStream.Write(pdfBytes, 0, pdfBytes.Length);
        sourceStream.Position = 0;

        using var doc = new Document(sourceStream);

        // Create a visible signature field on page 1.
        var rect = new Rectangle(100, 100, 250, 150); // llx, lly, urx, ury
        var sigField = new SignatureField(doc.Pages[1], rect) { PartialName = "Signature1" };
        doc.Form.Add(sigField, 1);

        // Prepare the PKCS7 signature object.
        var pkcs7 = new PKCS7(pfxPath, pfxPassword)
        {
            Reason       = "Document approved",
            Location     = "Company HQ",
            ContactInfo  = "contact@example.com",
            // Optional visual customisation.
            CustomAppearance = new SignatureCustomAppearance
            {
                FontSize        = 8,
                ShowContactInfo = false,
                ShowLocation    = false
            }
        };

        // Apply the digital signature. The signature is written back into the source stream.
        sigField.Sign(pkcs7);

        // Write the signed PDF to the output file. No additional Document.Save call is needed.
        File.WriteAllBytes(outputPdfPath, sourceStream.ToArray());

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
