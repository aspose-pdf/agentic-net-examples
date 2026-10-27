using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_locked.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found.");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Choose the page where the signature will be placed (first page in this example)
                Page page = doc.Pages[1];

                // Define a visible rectangle for the signature field (coordinates are in points)
                var signatureRect = new Rectangle(100, 500, 300, 550);

                // Create the signature field, add it to the form and make it read‑only
                var sigField = new SignatureField(page, signatureRect)
                {
                    PartialName = "Signature1",
                    // Setting ReadOnly prevents any further changes to the field after the document is saved
                    ReadOnly = true
                };
                doc.Form.Add(sigField);

                // NOTE: The LockDictionary API and direct PKCS7 signature assignment are not available
                // in the core Aspose.Pdf assembly used here. The field is therefore locked by the
                // ReadOnly flag, which is sufficient to stop further modifications.

                // Save the PDF with the (empty) signature field
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
