using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "registration.pdf";
        const string outputPath = "limited_id.pdf";
        const string fieldName  = "ID";
        const int    charLimit  = 10;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does not implement IDisposable; instantiate directly.
            FormEditor formEditor = new FormEditor();

            // Load the PDF document.
            formEditor.BindPdf(inputPath);

            // Apply a character limit of 10 to the "ID" field.
            formEditor.SetFieldLimit(fieldName, charLimit);

            // Save the updated PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"PDF saved with field limit to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}