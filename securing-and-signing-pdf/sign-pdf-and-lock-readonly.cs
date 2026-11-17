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
        const string outputPath = "signed_locked.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        try
        {
            // 1. Load the original PDF and apply password‑based encryption that disables all edit permissions.
            //    This makes the document read‑only. Encryption must be done *before* signing, otherwise the signature
            //    would be broken (the PDF bytes are rewritten after signing).
            using (var doc = new Document(inputPath))
            {
                // No user password (empty string) – anyone can open the file for reading.
                // Owner password protects the document and is required to sign it later.
                // Permissions: only allow printing; all other modifications are prohibited.
                doc.Encrypt("", "owner-pass", Permissions.PrintDocument, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF into an expandable memory stream.
                using (var encryptedStream = new MemoryStream())
                {
                    doc.Save(encryptedStream);
                    encryptedStream.Position = 0;

                    // 2. Open the encrypted PDF with the owner password so we can add a digital signature.
                    using (var signedDoc = new Document(encryptedStream, "owner-pass"))
                    {
                        // 3. Create a visible signature field on the first page.
                        var rect = new Rectangle(100, 100, 300, 160); // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
                        var signatureField = new SignatureField(signedDoc.Pages[1], rect)
                        {
                            PartialName = "Signature1"
                        };
                        signedDoc.Form.Add(signatureField, 1);

                        // 4. Prepare the PKCS#7 signature object.
                        var pkcs7 = new PKCS7(certificatePath, certificatePassword)
                        {
                            Reason = "Document approval",
                            Location = "Office",
                            ContactInfo = "contact@example.com",
                            // Optional custom appearance – hide contact info, set a small font, etc.
                            CustomAppearance = new SignatureCustomAppearance
                            {
                                FontSize = 8,
                                ShowContactInfo = false,
                                ShowLocation = false,
                                ShowReason = false
                            }
                        };

                        // 5. Sign the field. The Sign method writes the signed PDF back into the source stream.
                        signatureField.Sign(pkcs7);

                        // 6. Write the final signed‑and‑locked PDF to disk.
                        File.WriteAllBytes(outputPath, encryptedStream.ToArray());
                    }
                }
            }

            Console.WriteLine($"Signed and read‑only PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
