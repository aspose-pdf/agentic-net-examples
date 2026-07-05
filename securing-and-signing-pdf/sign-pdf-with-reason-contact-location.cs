using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF to be signed
        const string inputPdfPath = "input.pdf";
        // Output signed PDF
        const string outputPdfPath = "signed_output.pdf";
        // Path to the PFX certificate file
        const string pfxPath = "certificate.pfx";
        // Password for the PFX file
        const string pfxPassword = "pfxPassword";

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX certificate not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the field to the page's annotation collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file and password
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                // Populate the required metadata fields
                Reason = "I agree to the terms and conditions.",
                ContactInfo = "john.doe@example.com",
                Location = "New York, USA"
                // Date, Authority, etc., can also be set if needed
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}