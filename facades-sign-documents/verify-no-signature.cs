using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePath = "sample.pdf";

        // Create a simple PDF with one blank page
        using (Document document = new Document())
        {
            document.Pages.Add();
            document.Save(samplePath);
        }

        // Load the PDF and check for signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(samplePath);

            bool containsSignature = pdfSignature.ContainsSignature();
            // VerifySignature is the non‑obsolete API. It throws if the field does not exist,
            // therefore call it only when a signature is actually present.
            bool verifyResult = false;
            if (containsSignature)
            {
                // The field name "Signature1" does not exist in this document, so verification will be false.
                // Using VerifySignature instead of the deprecated VerifySigned.
                verifyResult = pdfSignature.VerifySignature("Signature1");
            }

            Console.WriteLine("ContainsSignature: " + containsSignature);
            Console.WriteLine("VerifySignature returned: " + verifyResult);
        }
    }
}
