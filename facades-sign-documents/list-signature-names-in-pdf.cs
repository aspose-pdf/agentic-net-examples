using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class SignatureHelper
{
    /// <summary>
    /// Returns the names of all non‑empty signatures in the specified PDF file.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF document.</param>
    /// <returns>IList of SignatureName objects.</returns>
    public static IList<SignatureName> ListSignatureNames(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // PdfFileSignature implements IDisposable, so use a using block.
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF file to the facade.
            pdfSign.BindPdf(pdfPath);

            // GetSignatureNames returns IList<SignatureName>.
            // The default parameter (onlyActive = true) returns only active signatures.
            IList<SignatureName> names = pdfSign.GetSignatureNames();

            return names;
        }
    }

    // Example entry point demonstrating usage.
    static void Main()
    {
        const string inputPdf = "sample_signed.pdf";

        try
        {
            IList<SignatureName> signatureNames = ListSignatureNames(inputPdf);

            Console.WriteLine($"Found {signatureNames.Count} signature(s) in '{inputPdf}':");
            for (int i = 0; i < signatureNames.Count; i++)
            {
                Console.WriteLine($"  [{i + 1}] {signatureNames[i]}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}