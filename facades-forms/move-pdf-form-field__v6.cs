using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "signedMoved.pdf";
        const string fieldName = "Signature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; ensure proper disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF into the editor
            formEditor.BindPdf(inputPath);

            // Move the 'Signature' field to the rectangle (300,400)-(500,600)
            formEditor.MoveField(fieldName, 300, 400, 500, 600);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' moved and saved to '{outputPath}'.");
    }
}