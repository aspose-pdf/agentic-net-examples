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

        // Load the existing PDF, modify its metadata, and save it
        using (Document doc = new Document(inputPath))
        {
            // Set creation and modification dates to the current time
            DateTime now = DateTime.Now;
            doc.Info.CreationDate = now;
            doc.Info.ModDate      = now;

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated timestamps to '{outputPath}'.");
    }
}