using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file to inspect
        const string pdfPath = "input.pdf";

        ListSignatureNames(pdfPath);
    }

    /// <summary>
    /// Lists all non‑empty signature names present in the specified PDF file.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF document.</param>
    static void ListSignatureNames(string pdfPath)
    {
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileSignature implements IDisposable, so use a using block for deterministic cleanup
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file to the facade
            pdfSignature.BindPdf(pdfPath);

            // Retrieve the names of all non‑empty signatures (default: only active signatures)
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

            Console.WriteLine($"Total signatures found: {signatureNames.Count}");
            for (int i = 0; i < signatureNames.Count; i++)
            {
                // SignatureName can be represented as a string via ToString()
                Console.WriteLine($"Signature {i + 1}: {signatureNames[i]}");
            }
        }
    }
}