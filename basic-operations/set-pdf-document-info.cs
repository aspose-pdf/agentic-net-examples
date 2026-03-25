using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, set metadata, and save
        using (Document doc = new Document(inputPath))
        {
            doc.Info.Author = "John Doe";
            doc.Info.Title = "Sample PDF Document";
            doc.Info.Subject = "Demonstration of setting document info";

            doc.Save(outputPath);
        }

        // Re‑open the saved file to verify the metadata values
        using (Document verifyDoc = new Document(outputPath))
        {
            Console.WriteLine($"Author : {verifyDoc.Info.Author}");
            Console.WriteLine($"Title  : {verifyDoc.Info.Title}");
            Console.WriteLine($"Subject: {verifyDoc.Info.Subject}");
        }
    }
}