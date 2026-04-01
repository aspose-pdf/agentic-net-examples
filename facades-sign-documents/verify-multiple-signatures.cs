using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            IList<string> signatureNames = pdfSignature.GetSignNames();

            Console.WriteLine("Found " + signatureNames.Count + " signature(s).");

            for (int i = 0; i < signatureNames.Count; i++)
            {
                string name = signatureNames[i];
                bool isValid = pdfSignature.VerifySigned(name);
                Console.WriteLine("Signature name: " + name);
                Console.WriteLine("Valid: " + isValid);
            }
        }
    }
}