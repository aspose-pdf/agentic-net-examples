using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "password";
        const string imagePath  = "signature.png";

        if (!File.Exists(inputPdf) || !File.Exists(pfxPath) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the visible signature will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Load the image that will be used as the signature appearance
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // PKCS1 with an image defines the visible appearance
                PKCS1 pkcs1 = new PKCS1(imgStream)
                {
                    Reason   = "Approved",
                    Location = "Head Office"
                };

                // Load the certificate (PFX) and sign the field using the overload
                // that accepts a signature object, a certificate stream, and a password
                using (FileStream pfxStream = File.OpenRead(pfxPath))
                {
                    sigField.Sign(pkcs1, pfxStream, pfxPassword);
                }
            }

            // Save the signed PDF (lifecycle rule: use using, then Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}