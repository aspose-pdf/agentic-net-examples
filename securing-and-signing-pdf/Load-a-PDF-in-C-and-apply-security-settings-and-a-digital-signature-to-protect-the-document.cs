using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for security and signature
using Aspose.Pdf;        // For DocumentPrivilege, KeySize, Algorithm

class Program
{
    static void Main()
    {
        // Input PDF to protect
        const string inputPdf   = "input.pdf";

        // Paths for intermediate encrypted PDF and final signed PDF
        const string encryptedPdf = "encrypted.pdf";
        const string signedPdf    = "signed.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Digital certificate for signing (PKCS#12 file) and its password
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certpass";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // -----------------------------------------------------------------
        // STEP 1: Apply security (encryption) to the PDF
        // -----------------------------------------------------------------
        // Create a privilege set – start with all permissions forbidden
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        // Allow printing and content extraction as an example
        privilege.AllowPrint = true;
        privilege.AllowCopy  = true;

        // Encrypt the PDF using 256‑bit AES encryption
        // PdfFileSecurity constructor accepts input and output file names
        using (PdfFileSecurity security = new PdfFileSecurity(inputPdf, encryptedPdf))
        {
            // EncryptFile returns true on success; you may handle the result if needed
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                privilege,
                KeySize.x256,
                Algorithm.AES); // AES algorithm for 256‑bit key

            if (!encrypted)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        // -----------------------------------------------------------------
        // STEP 2: Apply a digital signature to the encrypted PDF
        // -----------------------------------------------------------------
        // Define the visible signature rectangle (System.Drawing.Rectangle)
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Bind the encrypted PDF file
            signer.BindPdf(encryptedPdf);

            // Optional: set an image that will appear as the signature appearance
            // signer.SignatureAppearance = "signatureImage.png";

            // Load the signing certificate
            signer.SetCertificate(certificatePath, certificatePassword);

            // Sign page 1 with the provided details
            signer.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "John Doe",
                SigLocation: "New York",
                visible: true,
                annotRect: signatureRect);

            // Save the signed PDF
            signer.Save(signedPdf);
        }

        Console.WriteLine($"PDF has been encrypted to '{encryptedPdf}' and signed to '{signedPdf}'.");
    }
}