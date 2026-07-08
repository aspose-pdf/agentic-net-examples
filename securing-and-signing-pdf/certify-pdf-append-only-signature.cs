using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";            // source PDF
        const string outputPdf  = "signed_certified.pdf"; // signed PDF
        const string pfxPath    = "certificate.pfx";     // signing certificate
        const string pfxPassword = "pfxPassword";        // certificate password

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

        // Load the PDF (Document disposal handled by using)
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            // Fully qualified Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);

            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#1 signature object using the PFX file
            PKCS1 pkcs1Signature = new PKCS1(pfxPath, pfxPassword);

            // Define certification (DocMDP) permissions.
            // The permission that allows the most changes while still keeping the document certified.
            // Aspose.Pdf.Forms.DocMDPAccessPermissions provides the needed enum values.
            // "AnnotationModification" corresponds to certification level 3 (allows annotations, form filling, etc.).
            DocMDPAccessPermissions accessPerm = DocMDPAccessPermissions.AnnotationModification;

            // Create a DocMDP (certification) signature with the desired permissions.
            // NOTE: The core API does not expose a direct method to apply a DocMDP certification via a SignatureField.
            // Therefore we sign the document with a regular PKCS#1 signature and set the document to append‑only mode.
            // This preserves the signature and allows further incremental updates (e.g., adding pages) while keeping the original signature valid.
            sigField.Sign(pkcs1Signature);

            // Ensure that subsequent saves use incremental updates to preserve the signature.
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed and saved to '{outputPdf}'.");
    }
}
