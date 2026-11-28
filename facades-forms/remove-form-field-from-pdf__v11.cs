using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "cleaned.pdf";
        const string fieldName = "ObsoleteField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so use a using block for proper cleanup.
        using (var formEditor = new FormEditor())
        {
            // Load the PDF into the editor.
            formEditor.BindPdf(inputPath);

            // Remove the specified form field (correct method name).
            formEditor.RemoveField(fieldName);

            // Save the cleaned PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' removed. Saved to '{outputPath}'.");
    }
}
