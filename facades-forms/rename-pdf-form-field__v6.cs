using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "renamed3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does not implement IDisposable, so no using block is needed.
            FormEditor formEditor = new FormEditor();

            // Load the PDF document.
            formEditor.BindPdf(inputPath);

            // Rename the form field from 'Email' to 'EmailAddress'.
            formEditor.RenameField("Email", "EmailAddress");

            // Save the updated PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"Renamed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}