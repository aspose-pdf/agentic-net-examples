using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "signed_removed.pdf";
        const string signatureToRemove = "MySignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(inputPath);

            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

            SignatureName foundSignature = null;
            foreach (SignatureName name in signatureNames)
            {
                if (name.ToString() == signatureToRemove)
                {
                    foundSignature = name;
                    break;
                }
            }

            if (foundSignature == null)
            {
                Console.WriteLine("Signature '{0}' not found. No changes made.", signatureToRemove);
            }
            else
            {
                pdfSignature.RemoveSignature(foundSignature, true);
                pdfSignature.Save(outputPath);
                Console.WriteLine("Signature removed and saved to '{0}'.", outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}
