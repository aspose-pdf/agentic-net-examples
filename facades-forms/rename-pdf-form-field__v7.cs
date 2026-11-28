using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "renamed4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does not implement IDisposable, so no using block needed
            FormEditor formEditor = new FormEditor();

            // Load the PDF document into the editor
            formEditor.BindPdf(inputPath);

            // Rename the form field from 'Phone' to 'ContactNumber'
            formEditor.RenameField("Phone", "ContactNumber");

            // Save the modified PDF
            formEditor.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}