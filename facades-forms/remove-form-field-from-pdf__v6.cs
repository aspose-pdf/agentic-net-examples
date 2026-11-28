using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "removed1.pdf";
        const string fieldName = "FirstName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the PDF document to the editor.
            editor.BindPdf(inputPath);

            // Remove the specified form field. The correct method name is RemoveField.
            editor.RemoveField(fieldName);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' removed. Saved as '{outputPath}'.");
    }
}
