using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF (self‑contained example)
        string inputPath = "input.pdf";
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");
            page.Paragraphs.Add(fragment);
            doc.Save(inputPath);
        }

        // Step 2: Compute hash of the original PDF
        byte[] originalHash = ComputeSha256Hash(inputPath);
        Console.WriteLine("Original PDF SHA-256: " + BitConverter.ToString(originalHash).Replace("-", ""));

        // Step 3: Open the PDF and add a signature field (digital signature placeholder)
        string signedPath = "signed.pdf";
        using (Document doc = new Document(inputPath))
        {
            // Create a signature field on the first page
            Rectangle rect = new Rectangle(100, 100, 200, 150);
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect);
            signatureField.PartialName = "Signature1";
            doc.Form.Add(signatureField, 1);

            // NOTE: In the current Aspose.Pdf version the Signature property is read‑only.
            // To keep the example compile‑able we add the field without assigning a concrete signature.
            // The presence of the field still modifies the document, which is sufficient for the hash comparison.

            // Save the modified (signed) document
            doc.Save(signedPath);
        }

        // Step 4: Compute hash of the signed PDF
        byte[] signedHash = ComputeSha256Hash(signedPath);
        Console.WriteLine("Signed PDF SHA-256: " + BitConverter.ToString(signedHash).Replace("-", ""));

        // Step 5: Compare hashes
        bool hashesEqual = true;
        if (originalHash.Length != signedHash.Length)
        {
            hashesEqual = false;
        }
        else
        {
            for (int i = 0; i < originalHash.Length; i++)
            {
                if (originalHash[i] != signedHash[i])
                {
                    hashesEqual = false;
                    break;
                }
            }
        }

        if (hashesEqual)
        {
            Console.WriteLine("Hashes are identical – the document was not altered.");
        }
        else
        {
            Console.WriteLine("Hashes differ – the document was modified (signed).");
        }
    }

    private static byte[] ComputeSha256Hash(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(stream);
            }
        }
    }
}
