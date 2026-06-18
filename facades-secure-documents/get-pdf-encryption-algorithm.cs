using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF meta‑information via the Facades API.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Check whether the document is encrypted.
            if (!pdfInfo.IsEncrypted)
            {
                Console.WriteLine("The PDF is not encrypted.");
                return;
            }

            // Access the underlying Document to read the CryptoAlgorithm property.
            Document doc = pdfInfo.Document;
            CryptoAlgorithm? algorithm = doc.CryptoAlgorithm; // Nullable enum

            if (algorithm.HasValue)
            {
                Console.WriteLine($"Encryption algorithm: {algorithm.Value}");
            }
            else
            {
                Console.WriteLine("Encryption algorithm could not be determined.");
            }
        }
    }
}