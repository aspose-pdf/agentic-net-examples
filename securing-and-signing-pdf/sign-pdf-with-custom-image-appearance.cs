using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "signed_output.pdf";  // signed PDF
        const string certPfxPath    = "certificate.pfx";    // signing certificate
        const string certPassword   = "pfxPassword";        // certificate password
        const string signatureImgPath = "signature.png";    // custom graphic for appearance

        // Verify required files exist
        if (!File.Exists(inputPdfPath) ||
            !File.Exists(certPfxPath) ||
            !File.Exists(signatureImgPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the page where the signature will be placed (first page in this example)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle for the signature field (left, bottom, width, height)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 50);

            // Create the signature field and add it to the page annotations
            SignatureField sigField = new SignatureField(page, sigRect);
            page.Annotations.Add(sigField);

            // Prepare the PKCS#1 signature object with the custom image
            using (FileStream imgStream = File.OpenRead(signatureImgPath))
            {
                PKCS1 pkcs1Signature = new PKCS1(imgStream);

                // Set optional signature properties
                pkcs1Signature.Reason = "Approved";
                pkcs1Signature.Location = "New York, USA";
                pkcs1Signature.ContactInfo = "contact@example.com";
                pkcs1Signature.Date = DateTime.Now;

                // Load the certificate (pfx) and associate it with the signature
                using (FileStream pfxStream = File.OpenRead(certPfxPath))
                {
                    // The PKCS1 constructor that accepts only the image does not include the certificate.
                    // Therefore we sign using the overload that takes the certificate stream.
                    // Create a PKCS1 signature that includes both image and certificate.
                    PKCS1 pkcs1WithCert = new PKCS1(pfxStream, certPassword);
                    // Apply the custom image to the signature appearance
                    pkcs1WithCert.CustomAppearance = new SignatureCustomAppearance
                    {
                        // The image is already set via the constructor; no further action needed.
                        // If you need to adjust appearance, configure properties here.
                    };
                    // Copy the previously set properties
                    pkcs1WithCert.Reason = pkcs1Signature.Reason;
                    pkcs1WithCert.Location = pkcs1Signature.Location;
                    pkcs1WithCert.ContactInfo = pkcs1Signature.ContactInfo;
                    pkcs1WithCert.Date = pkcs1Signature.Date;

                    // Sign the document using the signature field
                    sigField.Sign(pkcs1WithCert);
                }
            }

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}