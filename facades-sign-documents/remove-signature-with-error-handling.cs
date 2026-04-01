using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "signed_removed.pdf";
        const string targetSignature = "MySignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            try
            {
                pdfSign.BindPdf(inputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to bind PDF: {ex.Message}");
                return;
            }

            IList<SignatureName> existingNames = pdfSign.GetSignatureNames();
            SignatureName signatureToRemove = null;
            foreach (SignatureName name in existingNames)
            {
                // SignatureName exposes the signature identifier via the Name property
                if (string.Equals(name.Name, targetSignature, StringComparison.OrdinalIgnoreCase))
                {
                    signatureToRemove = name;
                    break;
                }
            }

            if (signatureToRemove == null)
            {
                Console.WriteLine($"Signature '{targetSignature}' not found. No changes made.");
            }
            else
            {
                try
                {
                    // Remove both the signature and its field from the document
                    pdfSign.RemoveSignature(signatureToRemove, true);
                    Console.WriteLine($"Signature '{targetSignature}' removed.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error removing signature: {ex.Message}");
                }
            }

            try
            {
                pdfSign.Save(outputPath);
                Console.WriteLine($"Result saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
            }
        }
    }
}
