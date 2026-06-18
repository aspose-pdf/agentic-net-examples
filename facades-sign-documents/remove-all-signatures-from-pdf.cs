using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that may contain digital signatures
        const string inputPath  = "signed_input.pdf";
        // Output PDF with all signatures removed
        const string outputPath = "unsigned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSignature is a facade class for signature operations
        // It implements IDisposable, so we wrap it in a using block
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the source PDF file
            pdfSign.BindPdf(inputPath);

            // Remove every signature present in the document
            pdfSign.RemoveSignatures();

            // Save the cleaned PDF to a new file
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"All signatures removed. Clean PDF saved to '{outputPath}'.");
    }
}