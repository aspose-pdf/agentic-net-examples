using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output4.pdf";
        const string fieldName  = "CountryList";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does NOT implement IDisposable; do NOT wrap in using.
            FormEditor formEditor = new FormEditor();

            // Bind the PDF file to the editor.
            formEditor.BindPdf(inputPath);

            // Remove the ListBox field named "CountryList".
            formEditor.RemoveField(fieldName);

            // Save the modified PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"Field '{fieldName}' removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
