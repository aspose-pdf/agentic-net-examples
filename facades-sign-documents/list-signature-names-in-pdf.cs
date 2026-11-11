using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    // Returns an array with the names of all digital signatures found in the PDF.
    static string[] GetSignatureNames(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"File not found: {pdfPath}");

        // PdfFileSignature is the correct facade for working with signatures.
        // It implements IDisposable, so we use a using block.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(pdfPath);

            // GetSignatureNames returns IList<SignatureName>. The boolean argument
            // indicates whether to include only visible signatures (true = all).
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames(true);

            List<string> result = new List<string>(signatureNames.Count);
            foreach (SignatureName name in signatureNames)
            {
                // SignatureName exposes the full name of the signature via the FullName property.
                result.Add(name.FullName);
            }
            return result.ToArray();
        }
    }

    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        try
        {
            string[] signatures = GetSignatureNames(inputPdf);

            if (signatures.Length == 0)
            {
                Console.WriteLine("No signatures found in the document.");
            }
            else
            {
                Console.WriteLine("Signatures present in the PDF:");
                foreach (string name in signatures)
                {
                    Console.WriteLine($"- {name}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}