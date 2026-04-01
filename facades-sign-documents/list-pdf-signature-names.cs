using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
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

        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

        Console.WriteLine("Total signatures: " + signatureNames.Count);
        for (int i = 0; i < signatureNames.Count; i++)
        {
            Console.WriteLine("Signature name: " + signatureNames[i]);
        }

        pdfSignature.Close();
    }
}
