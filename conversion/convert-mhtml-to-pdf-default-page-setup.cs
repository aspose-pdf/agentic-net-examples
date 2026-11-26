using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.mht";
        const string outputPath = "Result.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the MHTML file with default page setup using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save the document as PDF using the default page configuration
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}