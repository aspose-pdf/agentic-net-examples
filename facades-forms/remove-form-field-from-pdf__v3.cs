using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "referralId";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; use using for deterministic disposal
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Remove the specified form field
            editor.RemoveField(fieldName);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' removed and saved to '{outputPath}'.");
    }
}