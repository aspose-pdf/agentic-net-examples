using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class SignatureLister
{
    // Lists all non‑empty signature names in the specified PDF file.
    public static void ListSignatureNames(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
        {
            Console.Error.WriteLine("PDF path is null, empty or whitespace.");
            return;
        }

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: '{pdfPath}'.");
            return;
        }

        try
        {
            // PdfFileSignature implements IDisposable, so use a using block for deterministic cleanup.
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                // Bind the PDF file to the facade.
                pdfSign.BindPdf(pdfPath);

                // GetSignatureNames returns IList<SignatureName>. The default parameter (onlyActive = true)
                // returns only active signatures.
                IList<SignatureName> names = pdfSign.GetSignatureNames();

                Console.WriteLine($"Found {names.Count} signature(s) in '{pdfPath}':");
                for (int i = 0; i < names.Count; i++)
                {
                    // SignatureName can be printed directly; its ToString() yields the name.
                    Console.WriteLine($"  [{i + 1}] {names[i]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing '{pdfPath}': {ex.Message}");
        }
    }

    // Example usage.
    static void Main(string[] args)
    {
        // Allow the PDF path to be supplied via command‑line arguments; fall back to a default placeholder.
        string inputPdf = args.Length > 0 ? args[0] : "sample_signed.pdf";
        ListSignatureNames(inputPdf);
    }
}
