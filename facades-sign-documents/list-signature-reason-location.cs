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
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

        for (int i = 0; i < signatureNames.Count; i++)
        {
            SignatureName signName = signatureNames[i];
            string reason = pdfSignature.GetReason(signName);
            string location = pdfSignature.GetLocation(signName);
            Console.WriteLine("Signature: " + signName.ToString());
            Console.WriteLine("  Reason  : " + reason);
            Console.WriteLine("  Location: " + location);
        }

        Console.WriteLine("Total revisions: " + pdfSignature.GetTotalRevision());
    }
}