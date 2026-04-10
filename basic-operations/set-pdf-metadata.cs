using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set its metadata, and save it
        using (Document doc = new Document(inputPath))
        {
            // Set document information
            doc.Info.Author  = "John Doe";
            doc.Info.Title   = "Sample PDF";
            doc.Info.Subject = "Demonstration of setting metadata";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        // Re‑open the saved PDF to verify that the metadata was written
        using (Document verifyDoc = new Document(outputPath))
        {
            Console.WriteLine($"Author : {verifyDoc.Info.Author}");
            Console.WriteLine($"Title  : {verifyDoc.Info.Title}");
            Console.WriteLine($"Subject: {verifyDoc.Info.Subject}");
        }
    }
}