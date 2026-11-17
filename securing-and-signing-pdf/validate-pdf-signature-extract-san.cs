using System;
using System.IO;
using System.Security.Cryptography;
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

        // Load the PDF document.
        Document doc = new Document(pdfPath);

        // Find the first signature field in the document.
        SignatureField sigField = null;
        foreach (Field field in doc.Form.Fields)
        {
            if (field is SignatureField sf)
            {
                sigField = sf;
                break;
            }
        }

        if (sigField == null)
        {
            Console.WriteLine("No signature field found in the PDF.");
            return;
        }

        // Verify the signature.
        bool isSignatureValid = sigField.Signature.Verify();
        Console.WriteLine($"Signature valid: {isSignatureValid}");

        if (!isSignatureValid)
        {
            Console.WriteLine("Signature verification failed.");
            return;
        }

        // Extract the signing certificate using the correct Aspose.Pdf API.
        X509Certificate2 signerCert = sigField.ExtractCertificateObject();
        if (signerCert == null)
        {
            Console.WriteLine("Signer certificate not available.");
            return;
        }

        Console.WriteLine($"Signer: {signerCert.Subject}");

        // Subject Alternative Name (SAN) extension OID.
        const string sanOid = "2.5.29.17";

        X509Extension sanExtension = signerCert.Extensions[sanOid];
        if (sanExtension == null)
        {
            Console.WriteLine("Subject Alternative Name extension not present.");
            return;
        }

        // Decode the SAN extension. The raw data is ASN.1 encoded; we output the formatted string.
        AsnEncodedData asnData = new AsnEncodedData(sanExtension.Oid, sanExtension.RawData);
        Console.WriteLine("Subject Alternative Name (raw ASN.1):");
        Console.WriteLine(asnData.Format(true));
    }
}
