using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Initialize the signature facade on the loaded document
                PdfFileSignature pdfSign = new PdfFileSignature(doc);

                // Retrieve all non‑empty signature names
                IList<SignatureName> names = pdfSign.GetSignatureNames();

                // Iterate through each signature and verify it
                foreach (SignatureName sigName in names)
                {
                    // VerifySigned expects the signature name as a string, not the SignatureName object
                    bool isValid = pdfSign.VerifySigned(sigName.Name);
                    Console.WriteLine($"Signature: {sigName.Name}  Valid: {isValid}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
