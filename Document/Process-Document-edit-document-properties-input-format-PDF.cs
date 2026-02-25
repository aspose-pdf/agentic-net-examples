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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- Edit standard document properties -----
                doc.Info.Title    = "New Document Title";
                doc.Info.Author   = "John Doe";
                doc.Info.Subject  = "Updated subject line";
                doc.Info.Keywords = "Aspose, PDF, metadata";

                // ----- Add a custom metadata entry -----
                // DocumentInfo behaves like a dictionary; you can store arbitrary key/value pairs.
                doc.Info["CustomProperty"] = "CustomValue";

                // Save the modified PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Properties updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}