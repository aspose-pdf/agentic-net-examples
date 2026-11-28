using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "renamed2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(inputPath);
            formEditor.RenameField("LastName", "FamilyName");
            formEditor.Save(outputPath);
            Console.WriteLine($"Field renamed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}