using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "CenteredWindow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set the CenterWindow flag, and save the result.
        using (Document doc = new Document(inputPath))
        {
            doc.CenterWindow = true;   // Open the PDF with the window centered.
            doc.Save(outputPath);      // Save the modified document.
        }

        Console.WriteLine($"Saved PDF with centered window to '{outputPath}'.");
    }
}