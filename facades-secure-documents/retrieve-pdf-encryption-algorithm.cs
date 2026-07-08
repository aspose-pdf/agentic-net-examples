using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify that the PDF file exists before attempting to open it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: The file '{pdfPath}' was not found.");
            return;
        }

        // Load the PDF document directly. The Document class provides encryption information.
        Document doc = new Document(pdfPath);

        // Check whether the PDF is encrypted.
        if (doc.IsEncrypted)
        {
            // CryptoAlgorithm is a nullable enum; handle the possible null value.
            var algorithm = doc.CryptoAlgorithm;
            string algoName = algorithm.HasValue ? algorithm.Value.ToString() : "Unknown";
            Console.WriteLine($"Encryption algorithm: {algoName}");
        }
        else
        {
            Console.WriteLine("PDF is not encrypted.");
        }
    }
}
