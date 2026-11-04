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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set the Author metadata to the current user name
            doc.Info.Author = Environment.UserName;

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Author metadata updated and saved to '{outputPath}'.");
    }
}