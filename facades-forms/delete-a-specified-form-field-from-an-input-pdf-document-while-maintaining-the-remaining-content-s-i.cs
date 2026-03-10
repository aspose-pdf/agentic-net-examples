using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF, and the field to delete
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myField";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document into the editor
            formEditor.BindPdf(inputPath);

            // Remove the specified form field
            formEditor.RemoveField(fieldName);

            // Save the modified PDF to the output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' removed. Saved to '{outputPath}'.");
    }
}