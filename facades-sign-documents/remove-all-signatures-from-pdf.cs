using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class RemoveSignaturesUtility
{
    static void Main(string[] args)
    {
        // Allow paths to be supplied via command‑line arguments; fall back to defaults.
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputPath = args.Length > 1 ? args[1] : "clean_output.pdf";

        // Ensure the source PDF exists – if it does not, create a minimal placeholder PDF.
        EnsureInputPdfExists(inputPath);

        // Use PdfFileSignature facade inside a using block to guarantee proper disposal.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);

            // Remove every existing signature. Aspose provides GetSignatureNames() to enumerate them.
            RemoveAllSignatures(pdfSignature);

            // Save the cleaned document.
            pdfSignature.Save(outputPath);
        }

        Console.WriteLine($"Clean PDF saved to '{outputPath}'.");
    }

    private static void EnsureInputPdfExists(string path)
    {
        if (!File.Exists(path))
        {
            // Create a simple one‑page PDF so the utility can run without external files.
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(path);
            }
        }
    }

    private static void RemoveAllSignatures(PdfFileSignature signatureFacade)
    {
        // GetSignatureNames returns a collection of SignatureName objects present in the PDF.
        var names = signatureFacade.GetSignatureNames();
        if (names != null)
        {
            foreach (var sigName in names)
            {
                // Use the overload that accepts a SignatureName instance.
                signatureFacade.RemoveSignature(sigName);
            }
        }
    }
}
