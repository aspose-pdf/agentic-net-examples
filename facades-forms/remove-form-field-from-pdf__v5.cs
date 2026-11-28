using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output21.pdf";
        const string fieldName  = "PhoneNumber";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so a using block is not required.
        FormEditor formEditor = new FormEditor();

        // Bind the PDF document to the FormEditor.
        formEditor.BindPdf(inputPath);

        // Remove the specified form field using the correct API method.
        formEditor.RemoveField(fieldName);

        // Save the modified PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Field '{fieldName}' removed. Saved to '{outputPath}'.");
    }
}
