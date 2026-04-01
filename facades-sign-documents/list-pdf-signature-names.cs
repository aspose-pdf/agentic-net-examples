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
            Console.WriteLine("Signature names found: " + signatureNames.Count);
            for (int i = 0; i < signatureNames.Count; i++)
            {
                Console.WriteLine("Signature name: " + signatureNames[i]);
            }
        }
    }
}