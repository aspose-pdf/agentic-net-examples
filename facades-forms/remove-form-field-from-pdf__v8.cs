using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "removed3.pdf";
        const string fieldName = "Email";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF file into the editor.
            formEditor.BindPdf(inputPath);

            // Remove the form field named "Email".
            formEditor.RemoveField(fieldName);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' removed. Saved as '{outputPath}'.");
    }
}
