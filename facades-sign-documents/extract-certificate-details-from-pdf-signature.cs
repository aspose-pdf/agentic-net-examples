using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        // ------------------------------------------------------------
        // Create a minimal PDF with a signature field so the example can run
        // in the sandbox where no external files exist.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                // Add a single blank page.
                seed.Pages.Add();

                // Define a rectangle for the signature field (x, y, width, height).
                // Aspose.Pdf.Rectangle expects (llx, lly, urx, ury).
                var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create a signature field and add it to the form.
                var sigField = new SignatureField(seed.Pages[1], sigRect)
                {
                    PartialName = "Signature1"
                };
                seed.Form.Add(sigField);

                // Save the seed PDF that will be used for extraction.
                seed.Save(inputPdf);
            }
        }

        // Initialize the PdfFileSignature facade and bind the PDF file.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Retrieve the signature names as SignatureName objects (not strings).
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

            foreach (SignatureName sigName in signatureNames)
            {
                // Extract the X.509 certificate associated with the signature.
                if (pdfSignature.TryExtractCertificate(sigName, out X509Certificate2 certificate))
                {
                    Console.WriteLine($"Signature Field: {sigName.Name}");
                    Console.WriteLine($"  Issuer          : {certificate.Issuer}");
                    Console.WriteLine($"  Expiration Date: {certificate.NotAfter}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Signature Field: {sigName.Name} – No certificate found.");
                }
            }
        }
    }
}
