using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF, set metadata, and save
        using (Document doc = new Document(inputPath))
        {
            // Set document information
            doc.Info.Author  = "John Doe";
            doc.Info.Title   = "Sample Document";
            doc.Info.Subject = "Demonstration of setting metadata";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Re-open the saved PDF to verify the metadata
        using (Document verifyDoc = new Document(outputPath))
        {
            Console.WriteLine($"Author : {verifyDoc.Info.Author}");
            Console.WriteLine($"Title  : {verifyDoc.Info.Title}");
            Console.WriteLine($"Subject: {verifyDoc.Info.Subject}");
        }
    }
}