using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string signatureField = "LegalSignature";
        const string outputCerPath = "certificate.cer";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(pdfPath);

        bool fieldFound = false;
        // Iterate over all form fields and locate the signature field by its name.
        foreach (Field field in pdfDoc.Form.Fields)
        {
            if (field is SignatureField sigField && sigField.PartialName == signatureField)
            {
                fieldFound = true;
                // Extract the signing certificate directly from the signature field.
                X509Certificate2 cert = sigField.ExtractCertificateObject();
                if (cert == null)
                {
                    Console.Error.WriteLine("No certificate associated with the signature.");
                    return;
                }

                // Export the certificate in DER format and write it to a .cer file.
                byte[] certBytes = cert.Export(X509ContentType.Cert);
                File.WriteAllBytes(outputCerPath, certBytes);

                Console.WriteLine($"Certificate extracted and saved to '{outputCerPath}'.");
                break;
            }
        }

        if (!fieldFound)
        {
            Console.Error.WriteLine($"Signature field '{signatureField}' not found.");
        }
    }
}
