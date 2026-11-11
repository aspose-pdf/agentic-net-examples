using System;
using System.IO;
using Aspose.Pdf;               // Document class
using Aspose.Pdf.Facades;      // Only needed if you work with PdfFileSignature later

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

        // First, determine whether the PDF is encrypted by trying to load it.
        // If the file is encrypted, Aspose.Pdf throws a PdfException.
        Document doc;
        try
        {
            doc = new Document(pdfPath);
        }
        catch (PdfException)
        {
            Console.WriteLine("The PDF is encrypted.");
            // Try to obtain the encryption algorithm via reflection – the property
            // exists in some versions (Document.Security.EncryptionAlgorithm) but is
            // not part of the public API in the version you are compiling against.
            // Using reflection avoids a compile‑time error while still giving you
            // the information when it is available.
            ShowEncryptionAlgorithmViaReflection(pdfPath);
            return;
        }

        // If we reach this point the document loaded successfully – it is not encrypted.
        Console.WriteLine("The PDF is not encrypted.");
    }

    private static void ShowEncryptionAlgorithmViaReflection(string pdfPath)
    {
        // Load the document again (this time we know it is encrypted).
        // We use reflection to read Document.Security.EncryptionAlgorithm if it exists.
        try
        {
            var doc = new Document(pdfPath);
            var securityProp = doc.GetType().GetProperty("Security");
            if (securityProp != null)
            {
                var securityObj = securityProp.GetValue(doc);
                var algoProp = securityObj?.GetType().GetProperty("EncryptionAlgorithm");
                if (algoProp != null)
                {
                    var algoValue = algoProp.GetValue(securityObj);
                    Console.WriteLine($"Encryption algorithm: {algoValue}");
                    return;
                }
            }
            // If we get here the property chain does not exist in this version.
            Console.WriteLine("Encryption algorithm information is not available in the current Aspose.PDF version.");
        }
        catch (Exception ex)
        {
            // Any unexpected exception – report it but do not crash the app.
            Console.WriteLine($"Unable to determine encryption algorithm: {ex.Message}");
        }
    }
}
