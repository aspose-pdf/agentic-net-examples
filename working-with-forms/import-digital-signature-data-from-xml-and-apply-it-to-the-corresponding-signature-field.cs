using System;
using System.IO;
using Aspose.Pdf;                         // Core PDF API
using Aspose.Pdf.Forms;                  // SignatureField class
using Aspose.Pdf.Facades;                // PKCS7 class for signing
using Aspose.Pdf.Annotations;            // XfdfReader for importing XFDF data

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";          // PDF containing an empty signature field
        const string xfdfPath       = "signatureData.xfdf"; // XFDF (XML) with signature field values
        const string pfxPath        = "certificate.pfx";   // PKCS#12 certificate
        const string pfxPassword    = "password";          // Certificate password
        const string outputPdfPath  = "signed_output.pdf";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Import field values (including the signature field placeholder) from the XFDF stream
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                Aspose.Pdf.Annotations.XfdfReader.ReadFields(xfdfStream, doc);
            }

            // Retrieve the signature field by its fully qualified name.
            // The Form collection provides indexed access to fields by name.
            // Cast to SignatureField to gain access to the Sign method.
            SignatureField sigField = doc.Form["Signature1"] as SignatureField;

            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field 'Signature1' not found in the document.");
                return;
            }

            // Create a PKCS7 object using the certificate. PKCS7 is the concrete implementation
            // required for signing (Signature is abstract and cannot be instantiated).
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Approved by author",
                ContactInfo = "author@example.com",
                Location    = "Head Office"
            };

            // Apply the digital signature to the previously imported signature field.
            sigField.Sign(pkcs7);

            // Save the signed PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
