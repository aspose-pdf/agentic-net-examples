using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchEncryptAndSign
{
    static void Main()
    {
        // Folder containing source PDFs
        const string sourceFolder = @"C:\Pdf\Input";
        // Folder where encrypted PDFs will be placed temporarily
        const string encryptedFolder = @"C:\Pdf\Encrypted";
        // Folder where final signed PDFs will be saved
        const string signedFolder = @"C:\Pdf\Signed";

        // Ensure output directories exist
        Directory.CreateDirectory(encryptedFolder);
        Directory.CreateDirectory(signedFolder);

        // Encryption parameters (same for all files in this example)
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Digital signature parameters
        const string certificatePath = @"C:\Certificates\mycert.pfx";
        const string certificatePassword = "certPass";

        // Iterate over all PDF files in the source folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string encryptedPath = Path.Combine(encryptedFolder, fileName + "_enc.pdf");
            string signedPath = Path.Combine(signedFolder, fileName + "_signed.pdf");

            // ---------- Encrypt ----------
            using (Document doc = new Document(pdfPath))
            {
                // Apply encryption using AES-256
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                // Save encrypted PDF
                doc.Save(encryptedPath);
            }

            // ---------- Sign (core API, no Facades) ----------
            // Load the encrypted document
            Document encDoc = new Document(encryptedPath);

            // Define the rectangle where the signature will appear (coordinates are in points)
            // Here we reuse the same rectangle that was previously used with SignatureAppearance
            Rectangle sigRect = new Rectangle(100, 100, 200, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(encDoc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form collection
            encDoc.Form.Add(sigField, 1);

            // Prepare the PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Document approved",
                Location = "Company HQ",
                ContactInfo = string.Empty // suppress contact info text
            };

            // Optional: customize the visual appearance of the signature
            pkcs7.CustomAppearance = new SignatureCustomAppearance
            {
                // Example: light gray background with no extra labels
                BackgroundColor = Color.FromArgb(200, 230, 230, 230),
                ShowReason = true,
                ShowLocation = true,
                ShowContactInfo = false,
                DigitalSignedLabel = "Signed by",
                FontSize = 8
            };

            // Sign the field (this also embeds the appearance defined above)
            sigField.Sign(pkcs7);

            // Save the signed PDF
            encDoc.Save(signedPath);

            Console.WriteLine($"Processed: {fileName}.pdf -> encrypted + signed.");
        }

        Console.WriteLine("Batch encryption and signing completed.");
    }
}
