using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_final.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a PKCS#1 signature object using the certificate
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPassword);
            pkcs1Signature.Reason = "Document certified – no further changes allowed";
            pkcs1Signature.ContactInfo = "contact@example.com";
            pkcs1Signature.Location = "Head Office";

            // Create a DocMDP signature that disallows any changes (NoChanges = 1)
            DocMDPSignature mdpSignature = new DocMDPSignature(
                pkcs1Signature,
                DocMDPAccessPermissions.NoChanges);

            // Define the visible signature rectangle using System.Drawing.Rectangle (left, top, width, height)
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Use the PdfFileSignature facade (fully qualified, no using directive for Facades)
            Aspose.Pdf.Facades.PdfFileSignature pdfSigner = new Aspose.Pdf.Facades.PdfFileSignature();
            pdfSigner.BindPdf(doc);

            // Certify the document with the MDP signature (page 1, visible signature)
            pdfSigner.Certify(
                page: 1,
                SigReason: pkcs1Signature.Reason,
                SigContact: pkcs1Signature.ContactInfo,
                SigLocation: pkcs1Signature.Location,
                visible: true,
                annotRect: signatureRect,
                docMdpSignature: mdpSignature);

            // Save the certified PDF
            pdfSigner.Save(outputPdf);
            pdfSigner.Close();
        }

        Console.WriteLine($"PDF signed and finalized: {outputPdf}");
    }
}
