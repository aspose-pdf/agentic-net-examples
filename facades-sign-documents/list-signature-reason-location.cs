using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Signatures;

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

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);
            IList<SignatureName> signatureNames = pdfSign.GetSignatureNames();

            for (int i = 0; i < signatureNames.Count; i++)
            {
                SignatureName signName = signatureNames[i];
                string reason = pdfSign.GetReason(signName);
                string location = pdfSign.GetLocation(signName);

                Console.WriteLine("Signature name: " + signName);
                Console.WriteLine("Reason: " + reason);
                Console.WriteLine("Location: " + location);
                Console.WriteLine();
            }

            Console.WriteLine("Total revisions: " + pdfSign.GetTotalRevision());
        }
    }
}