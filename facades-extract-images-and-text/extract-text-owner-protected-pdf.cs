using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the owner password
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Extract all text from the document
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                Console.WriteLine("--- Extracted Text ---");
                Console.WriteLine(extractedText);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}