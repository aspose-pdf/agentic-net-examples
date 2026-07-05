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

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the FormEditor facade
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Remove the specified form field
                editor.RemoveField(fieldName);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Field '{fieldName}' removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}