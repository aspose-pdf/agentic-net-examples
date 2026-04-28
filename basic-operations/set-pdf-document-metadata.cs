using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the directory for the output file exists (pre‑empt copy errors)
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Open the PDF, set metadata, and save (lifecycle rule: save inside using)
        using (Document doc = new Document(inputPath))
        {
            doc.Info.Author = "John Doe";
            doc.Info.Title = "Sample Document";
            doc.Info.Subject = "Demonstration of setting metadata";

            doc.Save(outputPath);
        }

        // Re‑open the saved PDF to verify the metadata
        using (Document verifyDoc = new Document(outputPath))
        {
            Console.WriteLine($"Author : {verifyDoc.Info.Author}");
            Console.WriteLine($"Title  : {verifyDoc.Info.Title}");
            Console.WriteLine($"Subject: {verifyDoc.Info.Subject}");
        }
    }
}
