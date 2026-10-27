using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class PdfSignatureHelper
{
    /// <summary>
    /// Returns a list of all digital signature field names present in the specified PDF.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>List of signature names; empty list if none or on error.</returns>
    public static List<string> ListSignatureNames(string pdfPath)
    {
        var signatures = new List<string>();

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return signatures;
        }

        try
        {
            // PdfFileSignature implements IDisposable, so wrap it in a using block.
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                // Bind the PDF document to the signature object.
                pdfSignature.BindPdf(pdfPath);

                // GetSignatureNames returns IList<SignatureName> (not a string[]).
                // Convert each SignatureName to its textual representation.
                IList<SignatureName> nameObjects = pdfSignature.GetSignatureNames();

                if (nameObjects != null)
                {
                    foreach (SignatureName sigName in nameObjects)
                    {
                        // SignatureName overrides ToString() to return the field name,
                        // but using the Name property is more explicit when available.
                        signatures.Add(sigName?.Name ?? sigName?.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading signatures: {ex.Message}");
        }

        return signatures;
    }

    // Example usage
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        List<string> sigNames = ListSignatureNames(inputPdf);

        Console.WriteLine($"Found {sigNames.Count} signature(s):");
        foreach (string name in sigNames)
        {
            Console.WriteLine($"- {name}");
        }
    }
}
