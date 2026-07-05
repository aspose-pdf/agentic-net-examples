using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // DigestHashAlgorithm

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string signedPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Compute hash of the original document ----------
        byte[] originalHash;
        using (Document doc = new Document(inputPath))
        using (MemoryStream ms = new MemoryStream())
        {
            // Save the document to a memory stream to obtain its raw bytes
            doc.Save(ms);
            byte[] originalBytes = ms.ToArray();

            using (SHA256 sha = SHA256.Create())
            {
                originalHash = sha.ComputeHash(originalBytes);
            }
        }

        // ---------- Sign the document ----------
        using (Document doc = new Document(inputPath))
        {
            // Create a PKCS#7 detached signature that uses SHA‑256
            PKCS7Detached pkcs7 = new PKCS7Detached(DigestHashAlgorithm.Sha256)
            {
                Reason   = "Document integrity verification",
                Location = "Office",
                Date     = DateTime.UtcNow
            };

            // Define the rectangle for the visible signature field
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 50);

            // Create the signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };

            // Add the field to the form collection
            doc.Form.Add(sigField, 1);

            // Apply the signature to the field (this populates the ByteRange, etc.)
            sigField.Sign(pkcs7);

            // Save the signed document
            doc.Save(signedPath);
        }

        // ---------- Compute hash of the signed content (ByteRange) ----------
        byte[] signedContentHash;
        using (Document signedDoc = new Document(signedPath))
        {
            // Retrieve the signature field we added earlier
            SignatureField signedField = null;
            foreach (Field f in signedDoc.Form.Fields)
            {
                if (f is SignatureField sf && sf.PartialName == "Signature1")
                {
                    signedField = sf;
                    break;
                }
            }

            if (signedField == null)
            {
                Console.Error.WriteLine("Signature field not found in signed document.");
                return;
            }

            // Load the entire signed file as raw bytes
            byte[] signedFileBytes = File.ReadAllBytes(signedPath);

            // ByteRange defines pairs: offset, length
            int[] range = signedField.Signature.ByteRange;
            using (MemoryStream rangeStream = new MemoryStream())
            {
                for (int i = 0; i < range.Length; i += 2)
                {
                    int offset = range[i];
                    int length = range[i + 1];
                    rangeStream.Write(signedFileBytes, offset, length);
                }

                byte[] rangeBytes = rangeStream.ToArray();
                using (SHA256 sha = SHA256.Create())
                {
                    signedContentHash = sha.ComputeHash(rangeBytes);
                }
            }
        }

        // ---------- Compare hashes ----------
        bool hashesMatch = StructuralComparisons.StructuralEqualityComparer.Equals(originalHash, signedContentHash);
        Console.WriteLine(hashesMatch
            ? "Integrity verified: hashes match."
            : "Integrity check failed: hashes do not match.");
    }
}
