using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output10.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; use using for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the PDF document to the editor
            formEditor.BindPdf(inputPath);

            // Set JavaScript for the form field named "CustomerName"
            formEditor.SetFieldScript("CustomerName", "event.value='John';");

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Saved output PDF to '{outputPath}'.");
    }
}