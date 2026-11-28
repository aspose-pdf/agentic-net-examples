using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output27.pdf";
        const string fieldName  = "EmailAddress";
        const string script     = "event.value='example@domain.com';";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the PDF to a FormEditor, set the JavaScript for the specified field, and save.
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);
        formEditor.SetFieldScript(fieldName, script);
        formEditor.Save(outputPath);
        // If FormEditor implements IDisposable you could call formEditor.Dispose() here.

        Console.WriteLine($"Field script applied and saved to '{outputPath}'.");
    }
}