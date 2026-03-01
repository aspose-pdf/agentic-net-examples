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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access and display standard metadata fields via Document.Info
                Console.WriteLine("Current metadata:");
                Console.WriteLine($"Title        : {doc.Info.Title}");
                Console.WriteLine($"Author       : {doc.Info.Author}");
                Console.WriteLine($"Subject      : {doc.Info.Subject}");
                Console.WriteLine($"Keywords     : {doc.Info.Keywords}");
                Console.WriteLine($"Creator      : {doc.Info.Creator}");
                Console.WriteLine($"Producer     : {doc.Info.Producer}");
                Console.WriteLine($"CreationDate : {doc.Info.CreationDate}");
                Console.WriteLine($"ModDate      : {doc.Info.ModDate}");

                // Modify some of the standard metadata fields
                doc.Info.Title = "Updated Document Title";
                doc.Info.Author = "John Doe";
                doc.Info.Subject = "Sample subject for accessibility";
                doc.Info.Keywords = "Aspose, PDF, metadata";

                // Add a custom metadata entry (only if the key is not predefined)
                if (!DocumentInfo.IsPredefinedKey("MyCustomKey"))
                {
                    doc.Info["MyCustomKey"] = "CustomValue";
                }

                // Save the updated PDF document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}