using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF metadata using the Facades API
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // Verify that the document is encrypted
            if (!fileInfo.IsEncrypted)
            {
                Console.WriteLine("The PDF is not encrypted.");
                return;
            }

            // Access the underlying Document to read the CryptoAlgorithm property
            Document doc = fileInfo.Document;
            CryptoAlgorithm? algorithm = doc.CryptoAlgorithm; // Nullable enum

            if (algorithm.HasValue)
            {
                Console.WriteLine($"Current encryption algorithm: {algorithm.Value}");
            }
            else
            {
                Console.WriteLine("Encryption algorithm could not be determined.");
            }
        }
    }
}