using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Collect all signature fields present in the document
            List<SignatureField> signatureFields = new List<SignatureField>();
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (var field in doc.Form.Fields)
                {
                    if (field is SignatureField sigField)
                        signatureFields.Add(sigField);
                }
            }

            if (signatureFields.Count == 0)
            {
                Console.WriteLine("No digital signatures found in the PDF.");
                return;
            }

            foreach (SignatureField sigField in signatureFields)
            {
                string fieldName = sigField.FullName ?? "<unnamed>";
                Console.WriteLine($"Signature field '{fieldName}' found.");

                // -----------------------------------------------------------------
                // 1. Extract the signing certificate (if any)
                // -----------------------------------------------------------------
                X509Certificate2 cert = null;
                try
                {
                    using (Stream certStream = sigField.ExtractCertificate())
                    using (MemoryStream ms = new MemoryStream())
                    {
                        certStream.CopyTo(ms);
                        cert = new X509Certificate2(ms.ToArray());
                    }

                    Console.WriteLine($"  Certificate subject: {cert.Subject}");
                    Console.WriteLine($"  Certificate validity: {cert.NotBefore:u} – {cert.NotAfter:u}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Unable to extract certificate: {ex.Message}");
                }

                // -----------------------------------------------------------------
                // 2. Retrieve the signing time (if the signature object exposes it)
                // -----------------------------------------------------------------
                DateTime signingTime = DateTime.MinValue;
                try
                {
                    // The Signature object (returned by the field) may expose a SigningTime property.
                    // Because the exact property name can differ between library versions, we use
                    // reflection as a safe fallback.
                    var signatureObj = sigField.Signature; // read‑only property
                    if (signatureObj != null)
                    {
                        var prop = signatureObj.GetType().GetProperty("SigningTime") ??
                                   signatureObj.GetType().GetProperty("SignatureDate") ??
                                   signatureObj.GetType().GetProperty("SignDate");
                        if (prop != null && prop.PropertyType == typeof(DateTime))
                        {
                            signingTime = (DateTime)prop.GetValue(signatureObj);
                        }
                    }

                    if (signingTime != DateTime.MinValue)
                        Console.WriteLine($"  Signing time: {signingTime:u}");
                    else
                        Console.WriteLine("  Signing time: <not available>");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Unable to read signing time: {ex.Message}");
                }

                // -----------------------------------------------------------------
                // 3. Basic validation – a signature is considered "valid" when a certificate
                //    could be extracted and the signing time lies inside the certificate's
                //    validity period. Full PKI validation (revocation, LTV, etc.) requires the
                //    PdfFileSignature facade, which is prohibited by the hard rule.
                // -----------------------------------------------------------------
                bool isValid = false;
                if (cert != null && signingTime != DateTime.MinValue)
                {
                    isValid = signingTime >= cert.NotBefore && signingTime <= cert.NotAfter;
                }
                Console.WriteLine($"  Signature valid (time within cert validity): {isValid}");

                Console.WriteLine(); // blank line between signatures
            }
        }
    }
}
