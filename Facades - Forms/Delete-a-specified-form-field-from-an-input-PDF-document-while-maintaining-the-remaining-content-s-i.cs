using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "textField"; // Name of the field to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize FormEditor with source PDF and target PDF
            using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
            {
                // Remove the specified form field
                formEditor.RemoveField(fieldName);

                // Persist changes to the output file
                formEditor.Save();
            }

            Console.WriteLine($"Field '{fieldName}' removed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}