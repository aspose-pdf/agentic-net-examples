using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchEncryptAndSign
{
    static void Main()
    {
        // Paths configuration
        const string inputFolder   = @"C:\InputPdfs";
        const string outputFolder  = @"C:\OutputPdfs";
        const string certPath      = @"C:\certificates\mycert.pfx";
        const string certPassword  = "certPass";

        // Encryption passwords
        const string userPassword  = "userPass";
        const string ownerPassword = "ownerPass";

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfFile);
            string encryptedPath = Path.Combine(outputFolder, fileNameWithoutExt + "_encrypted.pdf");
            string signedPath    = Path.Combine(outputFolder, fileNameWithoutExt + "_signed.pdf");

            try
            {
                // ---------- Encrypt ----------
                using (Document doc = new Document(pdfFile))
                {
                    Permissions perms = Permissions.PrintDocument |
                                         Permissions.ModifyContent |
                                         Permissions.ExtractContent;

                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(encryptedPath);
                }

                // ---------- Sign ----------
                using (Document encDoc = new Document(encryptedPath, userPassword))
                {
                    // Create a signature field on the first page
                    Rectangle sigRect = new Rectangle(100, 100, 300, 150);
                    SignatureField sigField = new SignatureField(encDoc.Pages[1], sigRect);
                    sigField.PartialName = "Signature1";

                    // Add the signature field to the document's form
                    encDoc.Form.Add(sigField, 1);

                    // Create PKCS7 signature using the certificate file
                    PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
                    pkcs7.Reason = "Document signed after encryption";

                    // Sign the field (Signature property is read‑only, so we call Sign)
                    sigField.Sign(pkcs7);

                    // Save the signed PDF
                    encDoc.Save(signedPath);
                }

                // Optionally delete the intermediate encrypted file
                if (File.Exists(encryptedPath))
                    File.Delete(encryptedPath);

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)} -> Signed PDF saved as {Path.GetFileName(signedPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }
}
