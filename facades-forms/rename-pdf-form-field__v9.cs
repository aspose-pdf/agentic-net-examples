using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "relabeled.pdf";
        const string oldFieldName = "OldLabel";
        const string newFieldName = "NewLabel";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does not implement IDisposable, so no using block is needed.
            FormEditor formEditor = new FormEditor();

            // Load the PDF into the editor.
            formEditor.BindPdf(inputPath);

            // Rename the specified form field.
            formEditor.RenameField(oldFieldName, newFieldName);

            // Save the updated PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"Field '{oldFieldName}' renamed to '{newFieldName}' and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}