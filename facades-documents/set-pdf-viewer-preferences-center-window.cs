using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "fullscreencenter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Set viewer preferences: no side panel and center the window.
        pdf.PageMode = PageMode.UseNone; // disables side panel
        pdf.CenterWindow = true;         // centers the PDF viewer window

        // Save the modified PDF.
        pdf.Save(outputPath);

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
