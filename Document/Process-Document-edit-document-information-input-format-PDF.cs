using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, edit its metadata, and save.
        using (Document doc = new Document(inputPath))
        {
            // Edit standard document information.
            doc.Info.Title   = "Updated Document Title";
            doc.Info.Author  = "John Doe";
            doc.Info.Subject = "Sample Subject";
            doc.Info.Keywords = "Aspose, PDF, Metadata";

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document information updated and saved to '{outputPath}'.");
    }
}