using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

// Input PDF, output PDF, and certificate (PFX) paths
const string inputPdfPath   = "input.pdf";
const string outputPdfPath  = "signed_sha512.pdf";
const string certificatePath = "cert.pfx";
const string certificatePassword = "password";

if (!File.Exists(inputPdfPath))
{
    Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
    return;
}
if (!File.Exists(certificatePath))
{
    Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
    return;
}

// Load the PDF document inside a using block for deterministic disposal
using (Document pdfDoc = new Document(inputPdfPath))
{
    // Create a signature field on the first page.
    // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle.
    Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
    SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect)
    {
        PartialName = "Signature1"
    };
    pdfDoc.Form.Add(sigField);

    // Load the signing certificate (PFX) with its password.
    X509Certificate2 cert = new X509Certificate2(certificatePath, certificatePassword);

    // Create an ExternalSignature specifying SHA‑512 as the digest algorithm.
    // DigestHashAlgorithm is in the Aspose.Pdf namespace.
    ExternalSignature signature = new ExternalSignature(cert, Aspose.Pdf.DigestHashAlgorithm.Sha512)
    {
        Reason      = "Document approved",
        Location    = "Head Office",
        ContactInfo = "security@example.com"
    };

    // Sign the PDF using the signature field and the configured signature object.
    sigField.Sign(signature);

    // Save the signed PDF. The using block ensures the document is properly closed.
    pdfDoc.Save(outputPdfPath);
}

Console.WriteLine($"PDF signed with SHA‑512 and saved to '{outputPdfPath}'.");