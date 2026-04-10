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

        // Bind the PDF file to the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Retrieve all non‑empty signature names
            IList<SignatureName> names = pdfSign.GetSignatureNames();

            if (names == null || names.Count == 0)
            {
                Console.WriteLine("No signatures found in the document.");
                return;
            }

            // Iterate through each signature and verify it
            foreach (SignatureName sigName in names)
            {
                string nameStr = sigName.ToString();

                // Verify the signature validity
                bool isValid = pdfSign.VerifySigned(nameStr);

                Console.WriteLine($"Signature name: {nameStr}");
                Console.WriteLine($"  Valid: {isValid}");
                Console.WriteLine($"  Signer: {pdfSign.GetSignerName(sigName)}");
                Console.WriteLine($"  Reason: {pdfSign.GetReason(sigName)}");
                Console.WriteLine($"  Location: {pdfSign.GetLocation(sigName)}");
                Console.WriteLine($"  DateTime: {pdfSign.GetDateTime(sigName)}");
            }
        }
    }
}