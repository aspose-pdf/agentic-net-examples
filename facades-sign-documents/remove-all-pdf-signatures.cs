using System;
using System.IO;
using Aspose.Pdf.Facades;

class RemovePdfSignatures
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "clean_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileSignature is the Facade class that can manipulate signatures.
        // It implements IDisposable, so we wrap it in a using block for deterministic cleanup.
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Bind the source PDF file.
            signer.BindPdf(inputPdf);

            // Remove all digital signatures from the document.
            signer.RemoveSignatures();

            // Save the cleaned PDF to a new file.
            signer.Save(outputPdf);
        }

        Console.WriteLine($"All signatures removed. Clean file saved as '{outputPdf}'.");
    }
}