using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchEncryptAndSign
{
    static void Main()
    {
        // Folder containing source PDF files
        const string sourceFolder = @"C:\PdfBatch\Input";
        // Folder where encrypted and signed PDFs will be written
        const string outputFolder = @"C:\PdfBatch\Output";

        // Encryption passwords
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Digital signature certificate (PFX) and its password
        const string certificatePath = @"C:\Certificates\mycert.pfx";
        const string certificatePassword = "certPass";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the source folder
        foreach (string inputFile in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFile);
            string encryptedPath = Path.Combine(outputFolder, fileNameWithoutExt + "_encrypted.pdf");
            string signedPath = Path.Combine(outputFolder, fileNameWithoutExt + "_signed.pdf");

            // ---------- Encrypt the PDF ----------
            using (Document doc = new Document(inputFile))
            {
                // Set desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // ---------- Sign the encrypted PDF ----------
            // Open the encrypted PDF with the user password
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                // Add a signature field on the first page
                Page firstPage = encDoc.Pages[1];
                // Define the rectangle where the signature will appear (left, bottom, right, top)
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
                // Create the signature field
                SignatureField sigField = new SignatureField(firstPage, sigRect)
                {
                    // Optional: set a name for the field
                    PartialName = "Signature1"
                };
                // Add the field to the page annotations
                firstPage.Annotations.Add(sigField);

                // Create a concrete PKCS7 signature object using the certificate
                PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
                // (Optional) set additional signature properties
                // pkcs7.Reason = "Document approved";
                // pkcs7.Location = "Office";
                // pkcs7.ContactInfo = "contact@example.com";

                // Sign the field
                sigField.Sign(pkcs7);

                // Save the signed PDF (creates a new file)
                encDoc.Save(signedPath);
            }

            Console.WriteLine($"Processed '{Path.GetFileName(inputFile)}' -> Encrypted: '{Path.GetFileName(encryptedPath)}', Signed: '{Path.GetFileName(signedPath)}'");
        }
    }
}
