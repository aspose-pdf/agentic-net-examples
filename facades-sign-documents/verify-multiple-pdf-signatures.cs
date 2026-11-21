using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to PdfFileSignature. The class does not implement IDisposable.
        var pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(pdfPath);

        // Retrieve all signature names present in the document.
        IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

        if (signatureNames == null || signatureNames.Count == 0)
        {
            Console.WriteLine("Document signed: false");
            Console.WriteLine("No signatures detected.");
            return;
        }

        Console.WriteLine("Document signed: true");
        Console.WriteLine("Signatures found:");

        // Verify each signature individually using the non‑obsolete VerifySignature method.
        foreach (SignatureName sigName in signatureNames)
        {
            bool isValid = pdfSignature.VerifySignature(sigName);
            Console.WriteLine($"- Name: {sigName.Name}, Valid: {isValid}");
        }
    }
}
