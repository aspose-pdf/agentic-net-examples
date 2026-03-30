using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string title = "Sample Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            document.SetTitle(title);
            document.DisplayDocTitle = true;
            document.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with title displayed: '{outputPath}'.");
    }
}