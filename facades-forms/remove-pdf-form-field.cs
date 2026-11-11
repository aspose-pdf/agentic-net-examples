using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "ObsoleteField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor is the correct facade class for form manipulation.
            // It implements IDisposable, so we use a using block.
            using (FormEditor formEditor = new FormEditor())
            {
                // Load the PDF document.
                formEditor.BindPdf(inputPath);

                // Remove the specified form field if it exists.
                formEditor.RemoveField(fieldName);

                // Save the modified PDF.
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"Field \"{fieldName}\" removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
