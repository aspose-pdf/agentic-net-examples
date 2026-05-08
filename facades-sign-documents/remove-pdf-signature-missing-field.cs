using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputPdf = "signed_document_updated.pdf";
        const string signatureNameToRemove = "Signature1";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create the facade and bind the PDF
            PdfFileSignature pdfSign = new PdfFileSignature();
            pdfSign.BindPdf(inputPdf);

            // Retrieve all existing signature names
            IList<SignatureName> existingNames = pdfSign.GetSignatureNames(true);

            // Check whether the requested signature exists
            bool signatureExists = existingNames.Any(sn => sn.ToString() == signatureNameToRemove);

            if (signatureExists)
            {
                // Remove the signature (only the signature, keep the field)
                // Use the overload that accepts a string name directly (no SignatureName constructor needed)
                pdfSign.RemoveSignature(signatureNameToRemove, false);
                Console.WriteLine($"Signature '{signatureNameToRemove}' removed.");
            }
            else
            {
                // Handle missing signature gracefully
                Console.WriteLine($"Signature '{signatureNameToRemove}' does not exist in the document.");
            }

            // Save the modified PDF
            pdfSign.Save(outputPdf);
            pdfSign.Close();

            Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
