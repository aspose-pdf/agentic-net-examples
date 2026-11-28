using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "renamed1.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create FormEditor facade
            FormEditor editor = new FormEditor();

            // Load the PDF document
            editor.BindPdf(inputPath);

            // Rename the form field
            editor.RenameField("FirstName", "GivenName");

            // Save the modified PDF
            editor.Save(outputPath);

            Console.WriteLine($"Renamed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}