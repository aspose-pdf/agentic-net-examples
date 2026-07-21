using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_locked.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
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
            // Create a PKCS7 signature object
            Signature signature = new PKCS7(certPath, certPass);
            signature.ContactInfo = "John Doe";
            signature.Location    = "Office";
            signature.Reason      = "Document approval";

            // Define the rectangle for the signature field (fully qualified type)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Add a signature field to the document
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.PartialName = "Signature1";
            sigField.Required    = true; // make the field required

            // Sign the field using the signature object
            sigField.Sign(signature);

            // Lock the document after signing (no further changes allowed)
            // DocMDPSignature with NoChanges permission enforces the lock
            DocMDPSignature mdp = new DocMDPSignature(signature, DocMDPAccessPermissions.NoChanges);
            // Ensure incremental updates are used and changes after signing raise an exception
            doc.Form.SignaturesAppendOnly = true;
            doc.HandleSignatureChange      = true;

            // Save the signed and locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}