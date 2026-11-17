using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class SecureAndSignPdf
{
    static void Main()
    {
        // Paths for the output PDF, certificate file and its password
        const string outputPdfPath = "secured_signed.pdf";
        const string certPath      = "certificate.pfx";
        const string certPassword  = "pfxPassword";

        // Ensure the certificate file exists
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Build the base PDF (content + encryption) and keep it in memory
        // -----------------------------------------------------------------
        byte[] basePdfBytes;
        using (var tempDoc = new Document())
        {
            // Add a single page
            Page page = tempDoc.Pages.Add();

            // Add some sample text to the page
            TextFragment tf = new TextFragment("This PDF is read‑only and digitally signed.");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Apply read‑only security (no permissions) using a strong algorithm
            Permissions readOnlyPerms = (Permissions)0; // no rights => read‑only
            string userPassword  = "user123"; // required to open the document
            string ownerPassword = "owner123"; // required to change security settings
            tempDoc.Encrypt(userPassword, ownerPassword, readOnlyPerms, CryptoAlgorithm.AESx256);

            // Save the document to a byte array
            using (var ms = new MemoryStream())
            {
                tempDoc.Save(ms);
                basePdfBytes = ms.ToArray();
            }
        }

        // -----------------------------------------------------------------
        // 2. Load the PDF into an expandable stream, add a signature field and sign
        // -----------------------------------------------------------------
        using (var expandableStream = new MemoryStream())
        {
            // Copy the bytes into an expandable MemoryStream (required for signing)
            expandableStream.Write(basePdfBytes, 0, basePdfBytes.Length);
            expandableStream.Position = 0;

            // Load the document from the expandable stream
            using (var doc = new Document(expandableStream))
            {
                // Define the rectangle where the signature will appear
                Rectangle sigRect = new Rectangle(100, 500, 300, 550);

                // Create the signature field on the first page (page numbers are 1‑based)
                SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
                {
                    PartialName = "Signature1"
                };

                // Add the signature field to the document's form collection
                doc.Form.Add(sigField, 1);

                // Prepare the PKCS#7 signature object
                PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                {
                    Reason      = "Document approved",
                    Location    = "Head Office",
                    ContactInfo = "admin@example.com",
                    // Optional custom appearance settings can be added here
                    // CustomAppearance = new SignatureCustomAppearance { FontSize = 8 }
                };

                // Sign the field – this writes the signed PDF back into the source stream
                sigField.Sign(pkcs7);
            }

            // After signing, write the final PDF bytes to the output file
            File.WriteAllBytes(outputPdfPath, expandableStream.ToArray());
        }

        Console.WriteLine($"PDF created, secured (read‑only) and signed: {outputPdfPath}");
    }
}
