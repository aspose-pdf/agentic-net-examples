using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Lists all non‑empty signature names in the specified PDF file.
    static void ListSignatureNames(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileSignature implements IDisposable, so use a using block.
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF document to the facade.
            pdfSign.BindPdf(pdfPath);

            // Retrieve the names of all non‑empty signatures.
            IList<SignatureName> names = pdfSign.GetSignatureNames();

            // Output each signature name.
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($"Signature name: {names[i]}");
            }
        }
    }

    static void Main(string[] args)
    {
        // Expect the PDF file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <pdf-file-path>");
            return;
        }

        ListSignatureNames(args[0]);
    }
}