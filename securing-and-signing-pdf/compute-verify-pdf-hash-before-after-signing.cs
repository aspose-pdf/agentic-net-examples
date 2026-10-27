using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string signedPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

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

        // Load the PDF and compute its hash before signing
        byte[] preSignHash;
        using (Document doc = new Document(inputPath))
        {
            preSignHash = ComputeDocumentHash(doc);

            // Create a signature field (position can be adjusted as needed)
            var sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Form.Add(sigField);

            // Sign the field using PKCS#1 (certificate file and password)
            PKCS1 pkcs = new PKCS1(certificatePath, certificatePassword);
            sigField.Sign(pkcs);

            // Save the signed PDF
            doc.Save(signedPath);
        }

        // Load the signed PDF and compute its hash after signing
        byte[] postSignHash;
        using (Document signedDoc = new Document(signedPath))
        {
            postSignHash = ComputeDocumentHash(signedDoc);
        }

        // Compare hashes
        bool hashesMatch = StructuralComparisons.StructuralEqualityComparer.Equals(preSignHash, postSignHash);
        Console.WriteLine(hashesMatch
            ? "Hashes match: document integrity unchanged after signing."
            : "Hashes differ: document content was altered during signing.");
    }

    // Helper method to compute SHA256 hash of a PDF document's bytes
    static byte[] ComputeDocumentHash(Document document)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // Save the document to a memory stream (PDF format)
            document.Save(ms);
            byte[] pdfBytes = ms.ToArray();

            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(pdfBytes);
            }
        }
    }
}