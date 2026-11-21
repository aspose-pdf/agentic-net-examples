using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_projectid.pdf";
        const string metaKey = "ProjectID";
        const string metaValue = "12345";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Add or update a custom metadata entry using the DocumentInfo indexer
                doc.Info[metaKey] = metaValue;

                // Save the updated PDF
                doc.Save(outputPath);
            }

            // ----- Verification -----
            using (Document verifyDoc = new Document(outputPath))
            {
                string retrieved = verifyDoc.Info[metaKey];
                Console.WriteLine($"Custom metadata '{metaKey}' = '{retrieved}'");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
