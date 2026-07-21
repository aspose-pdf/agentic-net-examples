using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // CryptoAlgorithm is a nullable enum; null means the document is not encrypted
                CryptoAlgorithm? algorithm = doc.CryptoAlgorithm;

                if (algorithm.HasValue)
                {
                    // Display the name of the encryption algorithm (e.g., AESx256, RC4x128)
                    Console.WriteLine($"Encryption algorithm: {algorithm.Value}");
                }
                else
                {
                    Console.WriteLine("Document is not encrypted.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}