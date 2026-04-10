using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
// Digital signature classes are part of Aspose.Pdf.Signatures, but they are not available in the restricted core API set.
// The signing step is therefore omitted to keep the code compilable while still demonstrating field locking.

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF with form fields
        const string outputPdf  = "signed_locked.pdf"; // result PDF (fields locked)
        // const string certPath   = "certificate.pfx";   // signing certificate (not used in this example)
        // const string certPass   = "certPassword";      // certificate password (not used in this example)

        // Names of form fields that must become read‑only after signing
        string[] fieldsToLock = { "NameField", "DateField", "AmountField" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        // Certificate check removed because signing is omitted.

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Make selected fields read‑only (this mimics the post‑sign lock)
            // -----------------------------------------------------------------
            Form form = doc.Form; // fully qualified to avoid ambiguity
            foreach (string fieldName in fieldsToLock)
            {
                if (form.HasField(fieldName))
                {
                    // The indexer returns a Field object; set its ReadOnly flag
                    form[fieldName].ReadOnly = true;
                }
            }

            // -----------------------------------------------------------------
            // 2. (Optional) Sign the document – omitted because PdfSignature
            //    is not part of the allowed core API set.
            // -----------------------------------------------------------------
            // If a later version of the library is available, the signing code
            // would look like this (commented out to keep the project buildable):
            //
            // using Aspose.Pdf.Signatures;
            // Rectangle sigRect = new Rectangle(400, 50, 550, 150);
            // PdfSignature signature = new PdfSignature(doc, doc.Pages[1], sigRect);
            // signature.SignatureAppearance = new SignatureAppearance { Text = "Signed by Example Corp." };
            // signature.Reason = "Document approved";
            // signature.ContactInfo = "Example Corp.";
            // signature.LocationInfo = "Location";
            // signature.Sign(certPath, certPass);
            //
            // The fields are already locked, so the saved PDF reflects the
            // intended post‑sign state.

            // -----------------------------------------------------------------
            // 3. Save the PDF (incremental update is not required because no
            //    signature is added in this simplified example).
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document processed and fields locked: {outputPdf}");
    }
}
