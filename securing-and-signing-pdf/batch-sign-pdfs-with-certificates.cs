using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSigner
{
    static void Main()
    {
        // List of PDF files together with their document‑type metadata.
        var pdfFiles = new List<(string Path, string DocType)>
        {
            ("invoice1.pdf", "Invoice"),
            ("contract1.pdf", "Contract")
        };

        foreach (var (pdfPath, docType) in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Retrieve the certificate (as a PFX stream) and its password for the given document type.
            // Replace GetCertificateInfo with real database access logic.
            var certInfo = GetCertificateInfo(docType);
            if (certInfo == null)
            {
                Console.Error.WriteLine($"No certificate found for document type '{docType}'.");
                continue;
            }

            // Load the PDF document (lifecycle rule: use using for deterministic disposal).
            using (Document doc = new Document(pdfPath))
            {
                // Sign every signature field present in the document.
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                    {
                        // Ensure the stream is at the beginning before creating the PKCS7 object.
                        certInfo.Stream.Position = 0;

                        // Create a PKCS7 signature instance from the PFX stream and password.
                        PKCS7 pkcs7 = new PKCS7(certInfo.Stream, certInfo.Password);

                        // Optional: set additional signature properties.
                        pkcs7.Reason = "Approved";
                        pkcs7.Location = Environment.MachineName;

                        // Apply the signature to the field.
                        sigField.Sign(pkcs7);
                    }
                }

                // Save the signed PDF (overwrite or create a new file).
                string signedPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath) ?? "",
                    Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

                doc.Save(signedPath);
                Console.WriteLine($"Signed PDF saved to '{signedPath}'.");
            }
        }
    }

    // Simple container for certificate data retrieved from the database.
    private class CertInfo
    {
        public Stream Stream { get; set; }   // PFX data stream
        public string Password { get; set; } // Password for the PFX
    }

    // Mock implementation – replace with actual DB query that returns the PFX stream and password.
    private static CertInfo GetCertificateInfo(string documentType)
    {
        // Example mapping from document type to a PFX file on disk.
        // In production, fetch the binary data and password from the database.
        string pfxPath = documentType switch
        {
            "Invoice"  => "certs/invoice.pfx",
            "Contract" => "certs/contract.pfx",
            _ => null
        };

        if (pfxPath == null || !File.Exists(pfxPath))
            return null;

        // Load the PFX file into a memory stream.
        MemoryStream ms = new MemoryStream(File.ReadAllBytes(pfxPath));

        // Placeholder password – in real scenarios this comes from secure storage.
        string password = "pfxPassword";

        return new CertInfo { Stream = ms, Password = password };
    }
}
