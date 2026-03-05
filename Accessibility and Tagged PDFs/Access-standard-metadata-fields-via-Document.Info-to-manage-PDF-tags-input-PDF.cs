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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access standard metadata via Document.Info
            DocumentInfo info = doc.Info;

            // Read existing metadata fields
            Console.WriteLine($"Title        : {info.Title}");
            Console.WriteLine($"Author       : {info.Author}");
            Console.WriteLine($"Subject      : {info.Subject}");
            Console.WriteLine($"Keywords     : {info.Keywords}");
            Console.WriteLine($"Creator      : {info.Creator}");
            Console.WriteLine($"Producer     : {info.Producer}");
            Console.WriteLine($"CreationDate : {info.CreationDate}");
            Console.WriteLine($"ModDate      : {info.ModDate}");

            // Modify metadata as needed
            info.Title = "Updated Document Title";
            info.Author = "John Doe";
            info.Subject = "Metadata Management Example";
            info.Keywords = "Aspose.Pdf, Metadata, C#";
            info.Creator = "My Application";
            info.Producer = "Aspose.Pdf for .NET";

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}