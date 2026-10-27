using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certPath = "ecc_cert.pfx";
        const string certPassword = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the ECC certificate (P‑256) containing the private key.
        X509Certificate2 eccCert = new X509Certificate2(
            certPath,
            certPassword,
            X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

        if (!eccCert.HasPrivateKey)
        {
            Console.Error.WriteLine("The certificate does not contain a private key.");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(inputPdfPath);

        // Define the rectangle for the visible signature field (llx, lly, urx, ury).
        Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

        // Create a signature field and add it to the document's form.
        SignatureField sigField = new SignatureField(pdfDoc, sigRect);
        pdfDoc.Form.Add(sigField);

        // Optional: give the field a name (visual text is supplied by the signature appearance).
        sigField.Name = "ECCSignature";

        // Create a PKCS1 signature handler using the certificate file path.
        PKCS1 pkcs1 = new PKCS1(certPath, certPassword);

        // Apply the digital signature.
        sigField.Sign(pkcs1);

        // Save the signed PDF.
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
