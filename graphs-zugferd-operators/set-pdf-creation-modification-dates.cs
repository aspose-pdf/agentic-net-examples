using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_timestamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set creation and modification dates, then save
        using (Document doc = new Document(inputPath))
        {
            DateTime now = DateTime.Now;
            doc.Info.CreationDate = now;          // set creation timestamp
            doc.Info.ModDate = now;               // set modification timestamp (correct property name)

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated timestamps to '{outputPath}'.");
    }
}
