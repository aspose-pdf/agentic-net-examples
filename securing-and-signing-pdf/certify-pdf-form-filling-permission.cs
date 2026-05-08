using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";               // source PDF
        const string outputPdf  = "signed_certified.pdf";    // signed PDF
        const string pfxPath    = "certificate.pfx";        // PKI certificate in PFX format
        const string pfxPassword = "pfxPassword";           // password for the PFX file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Prepare the PKCS#7 signature (concrete implementation of abstract Signature)
        // -----------------------------------------------------------------
        PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
        pkcs7.Reason   = "Document certified – form filling allowed only";
        pkcs7.Location = "Company HQ";
        pkcs7.ContactInfo = "contact@example.com";

        // -----------------------------------------------------------------
        // 2. Wrap the PKCS#7 signature in a DocMDP (certification) object with the required permission
        // -----------------------------------------------------------------
        DocMDPSignature mdpSignature = new DocMDPSignature(
            pkcs7,
            Aspose.Pdf.Forms.DocMDPAccessPermissions.FillingInForms);

        // -----------------------------------------------------------------
        // 3. Apply the certification signature using PdfFileSignature (core API only – fully qualified name, no using directive)
        // -----------------------------------------------------------------
        // The rectangle defines the visible appearance of the certification signature on page 1.
        // System.Drawing.Rectangle expects (x, y, width, height).
        System.Drawing.Rectangle visibleRect = new System.Drawing.Rectangle(100, 500, 200, 50);

        // Fully qualified Facades type – allowed as we do not import the namespace.
        Aspose.Pdf.Facades.PdfFileSignature signer = new Aspose.Pdf.Facades.PdfFileSignature();
        signer.BindPdf(inputPdf);
        // Certify(pageNumber, reason, contactInfo, location, visible, rect, mdpSignature)
        signer.Certify(
            1,                                 // page number (1‑based)
            pkcs7.Reason,                      // reason
            pkcs7.ContactInfo,                 // contact information
            pkcs7.Location,                    // location
            true,                              // make the signature visible
            visibleRect,                       // appearance rectangle
            mdpSignature);                     // certification (DocMDP) object
        signer.Save(outputPdf);
        signer.Close();

        Console.WriteLine($"Document signed and certified successfully: {outputPdf}");
    }
}
