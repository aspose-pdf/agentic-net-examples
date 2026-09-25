using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF, set document information, and save
            using (Document doc = new Document(inputPath))
            {
                // Set metadata
                doc.Info.Author  = "John Doe";
                doc.Info.Title   = "Sample Document";
                doc.Info.Subject = "Demonstration of setting PDF info";

                // Save the updated PDF
                doc.Save(outputPath);
            }

            // Re-open the saved PDF to verify the metadata
            using (Document verifyDoc = new Document(outputPath))
            {
                Console.WriteLine("Verification after saving:");
                Console.WriteLine($"Author : {verifyDoc.Info.Author}");
                Console.WriteLine($"Title  : {verifyDoc.Info.Title}");
                Console.WriteLine($"Subject: {verifyDoc.Info.Subject}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}