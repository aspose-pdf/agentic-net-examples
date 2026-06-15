using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf.Facades;   // PdfFileSignature, SignatureName
using Aspose.Pdf;           // Document (if needed for other operations)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_removed.pdf";
        const string targetSignature = "Signature1"; // name of the signature to remove

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade and bind the PDF
            PdfFileSignature pdfSign = new PdfFileSignature();
            pdfSign.BindPdf(inputPath);

            // Retrieve all existing signature names
            IList<SignatureName> existingNames = pdfSign.GetSignatureNames();

            // Check whether the requested signature exists
            bool signatureExists = existingNames
                .Any(sn => string.Equals(sn.ToString(), targetSignature, StringComparison.Ordinal));

            if (!signatureExists)
            {
                // Handle the missing signature case gracefully
                Console.WriteLine($"Signature \"{targetSignature}\" does not exist in the document.");
            }
            else
            {
                // Remove the signature using the string overload (no need to instantiate SignatureName)
                pdfSign.RemoveSignature(targetSignature);
                Console.WriteLine($"Signature \"{targetSignature}\" removed.");
            }

            // Save the resulting PDF
            pdfSign.Save(outputPath);
            Console.WriteLine($"Result saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // General error handling – report any unexpected issues
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
