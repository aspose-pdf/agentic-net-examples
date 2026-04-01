using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Create a sample PDF if it does not exist (unencrypted)
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
            Console.WriteLine($"Created sample PDF: {inputPath}");
        }

        // Load the PDF and retrieve its encryption algorithm
        using (Document doc = new Document(inputPath))
        {
            CryptoAlgorithm? algorithm = doc.CryptoAlgorithm;
            if (algorithm.HasValue)
            {
                Console.WriteLine($"Encryption algorithm: {algorithm.Value}");
            }
            else
            {
                Console.WriteLine("PDF is not encrypted.");
            }
        }
    }
}