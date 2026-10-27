using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed.pdf";
        const string outputPath = "clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Initialize the signature handler for the loaded document
                PdfFileSignature pdfSignature = new PdfFileSignature(doc);

                // Retrieve all signature names present in the document
                IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

                // Remove each signature one by one
                foreach (SignatureName sigName in signatureNames)
                {
                    // RemoveSignature expects the signature name as a string
                    pdfSignature.RemoveSignature(sigName.Name);
                }

                // Save the cleaned document to a new file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Clean PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}