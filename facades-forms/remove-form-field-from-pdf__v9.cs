using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "removed4.pdf";
        const string fieldName = "Phone";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Remove the form field named "Phone"
            editor.RemoveField(fieldName);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' removed. Saved to '{outputPath}'.");
    }
}
