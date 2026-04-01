using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "signed_output.pdf";
        // Path to a PKCS#12 certificate file and its password.
        // Replace with a valid certificate when running the code.
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        // ---------------------------------------------------------------------
        // 1. Create a blank PDF document with a single page.
        // ---------------------------------------------------------------------
        Document doc = new Document();
        doc.Pages.Add();

        // ---------------------------------------------------------------------
        // 2. Define the rectangle that will contain the visible signature.
        //    (llx, lly, urx, ury) – coordinates are measured from the bottom‑left.
        // ---------------------------------------------------------------------
        Rectangle signatureRect = new Rectangle(100, 500, 300, 550);

        // ---------------------------------------------------------------------
        // 3. Build a custom appearance with a semi‑transparent background.
        // ---------------------------------------------------------------------
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            // 50 % transparent light gray (alpha = 128 out of 255)
            BackgroundColor = Color.FromArgb(128, 200, 200, 200),
            ForegroundColor = Color.Black
        };

        // ---------------------------------------------------------------------
        // 4. Create the PKCS7 signature object and attach the custom appearance.
        // ---------------------------------------------------------------------
        if (File.Exists(pfxPath))
        {
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            pkcs7.CustomAppearance = customAppearance;

            // ---------------------------------------------------------------------
            // 5. Add a signature field to the document and sign it.
            // ---------------------------------------------------------------------
            SignatureField sigField = new SignatureField(doc, signatureRect)
            {
                Name = "Signature1"
            };
            doc.Form.Add(sigField);
            sigField.Sign(pkcs7);
        }
        else
        {
            Console.WriteLine($"Certificate file '{pfxPath}' not found. The PDF will be saved without a digital signature.");
        }

        // ---------------------------------------------------------------------
        // 6. Save the PDF.
        // ---------------------------------------------------------------------
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
