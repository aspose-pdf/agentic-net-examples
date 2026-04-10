using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "signed_output.pdf"; // signed PDF
        const string pfxPath       = "certificate.pfx";   // signing certificate
        const string pfxPassword   = "pfxPassword";       // certificate password

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle: using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle for the visible signature field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the field to the page's annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Load the certificate (lifecycle: using)
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // Create a PKCS#1 signature object using the certificate stream
                PKCS1 pkcs1Signature = new PKCS1(pfxStream, pfxPassword);

                // NOTE: Aspose.Pdf.Forms.DocMDPAccessPermissions does NOT contain an "AppendOnly" value.
                // The highest certification level that can be expressed with the core API is
                // AnnotationModification, which allows modifications such as adding annotations after signing.
                // If a true "add new pages" certification is required, the Facades API (PdfFileSignature.Certify)
                // would be needed, but that namespace is prohibited by the task constraints.
                // Therefore we sign the document with a regular PKCS#1 signature.
                sigField.Sign(pkcs1Signature);
            }

            // Save the signed PDF (lifecycle: using, no extra PreSave needed)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
    }
}
