using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_output.pdf"; // signed PDF
        const string appearanceImage = "signature.png"; // optional appearance image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Obtain a certificate from the smart card (current user store)
        // -----------------------------------------------------------------
        X509Certificate2 certificate = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            // Find all valid certificates; the smart‑card certificate will be among them
            X509Certificate2Collection validCerts = store.Certificates.Find(
                X509FindType.FindByTimeValid, DateTime.Now, false);

            // Let the user pick a certificate (required for smart‑card usage)
            X509Certificate2Collection selected = X509Certificate2UI.SelectFromCollection(
                validCerts, "Select Certificate", "Choose a certificate from the smart card", X509SelectionFlag.SingleSelection);

            if (selected.Count == 0)
            {
                Console.Error.WriteLine("No certificate selected.");
                return;
            }

            certificate = selected[0];
        }

        // -----------------------------------------------------------------
        // 2. Create an ExternalSignature that uses the selected certificate
        // -----------------------------------------------------------------
        ExternalSignature externalSig = new ExternalSignature(certificate)
        {
            Reason      = "Approved for release",
            ContactInfo = "john.doe@example.com",
            Location    = "Head Office"
        };

        // -----------------------------------------------------------------
        // 3. Sign the PDF using PdfFileSignature
        // -----------------------------------------------------------------
        // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
        Rectangle sigRect = new Rectangle(100, 100, 200, 100);

        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSigner.BindPdf(inputPdf);

            // Optional: set a visual appearance (image) for the signature
            if (File.Exists(appearanceImage))
                pdfSigner.SignatureAppearance = appearanceImage;

            // Sign page 1 (pages are 1‑based)
            pdfSigner.Sign(
                page:          1,
                SigReason:     externalSig.Reason,
                SigContact:    externalSig.ContactInfo,
                SigLocation:   externalSig.Location,
                visible:       true,
                annotRect:     sigRect,
                sig:           externalSig);

            // Save the signed document
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}