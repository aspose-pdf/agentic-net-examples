using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "bound_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create an AutoFiller instance and bind the PDF form from a file path
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(inputPath);

        // Optionally, you could save the document after filling or other operations.
        // Here we simply demonstrate the binding step.
        Console.WriteLine($"PDF form bound to AutoFiller from '{inputPath}'.");
    }
}