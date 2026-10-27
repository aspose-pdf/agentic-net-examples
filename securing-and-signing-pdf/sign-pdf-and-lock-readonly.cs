using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string signedPdfPath = "signed.pdf";
        const string lockedPdfPath = "locked.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Add a signature field (placeholder – no actual signing)
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the signature rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            var sigRect = new Rectangle(100, 100, 200, 150);

            // Create a signature field on the first page
            var signatureField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form
            doc.Form.Add(signatureField);

            // Save the PDF that now contains the (empty) signature field
            doc.Save(signedPdfPath);
        }

        // ------------------------------------------------------------
        // Step 2: Lock the signed PDF (read‑only) by encrypting with no permissions
        // ------------------------------------------------------------
        using (Document signedDoc = new Document(signedPdfPath))
        {
            // (Permissions)0 means "no permissions" – the document becomes read‑only
            Permissions noPermissions = (Permissions)0;

            signedDoc.Encrypt(
                userPassword: "",
                ownerPassword: "ownerPassword",
                permissions: noPermissions,
                cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // Save the locked PDF
            signedDoc.Save(lockedPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to: {signedPdfPath}");
        Console.WriteLine($"Locked (read‑only) PDF saved to: {lockedPdfPath}");
    }
}
