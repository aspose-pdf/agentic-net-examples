using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that contains signatures
        const string inputPath = "signed.pdf";
        // Output PDF after removal
        const string outputPath = "signed_removed.pdf";
        // Name of the signature to remove
        const string signatureToRemove = "Signature1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfFileSignature facade
            PdfFileSignature pdfSign = new PdfFileSignature();
            // Bind the PDF file to the facade
            pdfSign.BindPdf(inputPath);

            // Retrieve all existing signature names
            IList<SignatureName> names = pdfSign.GetSignatureNames();

            // Search for the requested signature name
            SignatureName target = null;
            foreach (SignatureName name in names)
            {
                // SignatureName exposes the Name property
                if (name.Name.Equals(signatureToRemove, StringComparison.OrdinalIgnoreCase))
                {
                    target = name;
                    break;
                }
            }

            if (target == null)
            {
                // Signature not found – handle gracefully
                Console.WriteLine($"Signature '{signatureToRemove}' does not exist. No changes made.");
            }
            else
            {
                // Remove the signature and its field (second argument = true)
                pdfSign.RemoveSignature(target, true);
                // Save the modified PDF
                pdfSign.Save(outputPath);
                Console.WriteLine($"Signature '{signatureToRemove}' removed successfully. Saved to '{outputPath}'.");
            }

            // Close the facade (optional but good practice)
            pdfSign.Close();
        }
    }
}