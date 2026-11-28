using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "captchaResponse";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor works on PDF files directly; it may implement IDisposable in newer versions.
        FormEditor editor = new FormEditor();
        try
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Remove the form field named "captchaResponse".
            editor.RemoveField(fieldName);

            // Save the modified PDF.
            editor.Save(outputPath);
        }
        finally
        {
            // Dispose if the class implements IDisposable.
            if (editor is IDisposable disposable)
                disposable.Dispose();
        }

        Console.WriteLine($"Field '{fieldName}' removed and saved to '{outputPath}'.");
    }
}