using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF meta‑information using the Facades API
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // The Document returned by PdfFileInfo also implements IDisposable.
            // Disposing it releases any file handles and ensures the assembly is not locked
            // when the application terminates, which prevents the MSB3026 copy‑lock warnings.
            using (Document doc = pdfInfo.Document)
            {
                // Retrieve the encryption algorithm (null if the PDF is not encrypted)
                CryptoAlgorithm? algorithm = doc.CryptoAlgorithm;

                string algorithmName = algorithm.HasValue
                    ? algorithm.Value.ToString()
                    : "None (PDF is not encrypted)";

                Console.WriteLine($"Encryption algorithm: {algorithmName}");
            }
        }
    }
}
