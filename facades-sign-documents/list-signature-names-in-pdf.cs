using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file whose signatures you want to list
        const string pdfPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // List all signature names in the PDF
        ListSignatureNames(pdfPath);
    }

    /// <summary>
    /// Binds the specified PDF file to a PdfFileSignature facade and prints
    /// the names of all non‑empty signatures (active signatures by default).
    /// </summary>
    /// <param name="pdfFile">Full path to the PDF document.</param>
    static void ListSignatureNames(string pdfFile)
    {
        // PdfFileSignature implements IDisposable via SaveableFacade,
        // so we wrap it in a using block for deterministic disposal.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file to the facade for processing.
            pdfSignature.BindPdf(pdfFile);

            // Retrieve the list of signature names.
            // The default parameter (onlyActive = true) returns only active signatures.
            var signatureNames = pdfSignature.GetSignatureNames();

            // Output each signature name.
            for (int i = 0; i < signatureNames.Count; i++)
            {
                Console.WriteLine($"Signature name: {signatureNames[i]}");
            }

            // No need to call Save() here because we are only reading information.
        }
    }
}