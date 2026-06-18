using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfFileSignature facade and bind the PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPath);

        // Retrieve all non‑empty signature names
        IList<SignatureName> names = pdfSign.GetSignatureNames();

        // Verify each signature and output the result
        foreach (SignatureName sigName in names)
        {
            string nameStr = sigName.ToString();
            bool isValid = pdfSign.VerifySigned(nameStr);
            Console.WriteLine($"Signature name: {nameStr}");
            Console.WriteLine($"  Valid: {isValid}");
        }

        // Release resources
        pdfSign.Close();
    }
}