using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and signing certificate (PFX) paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_invisible.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";

        // Ensure the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose a page to place the (invisible) signature field.
            // Page indexing in Aspose.Pdf is 1‑based.
            Page page = doc.Pages[1];

            // Define a zero‑size rectangle – the field will not be visible
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create the signature field and add it to the document's form.
            SignatureField sigField = new SignatureField(page, rect);
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file.
            // The constructor (string pfx, string password) matches the API.
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword);

            // Record the signing time.
            pkcs7Signature.Date = DateTime.Now;

            // Hide the signature appearance (invisible signature).
            pkcs7Signature.ShowProperties = false;

            // Optional: set additional metadata (reason, location, etc.).
            // pkcs7Signature.Reason = "Document approved";
            // pkcs7Signature.Location = "Office";

            // Sign the document using the signature field.
            sigField.Sign(pkcs7Signature);

            // Ensure the signature is preserved via incremental update.
            // This prevents layout changes when the document is saved later.
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF (lifecycle rule: use Document.Save(string)).
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Invisible digital signature applied and saved to '{outputPdfPath}'.");
    }
}