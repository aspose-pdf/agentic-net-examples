using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "removed2.pdf";
        const string fieldName  = "LastName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so use a using block for deterministic cleanup
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF into the editor
            formEditor.BindPdf(inputPath);

            // Remove the specified form field
            formEditor.RemoveField(fieldName);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' removed. Saved as '{outputPath}'.");
    }
}