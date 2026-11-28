using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "removed5.pdf";
        const string fieldName = "Address";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor is the correct facade for form structure operations
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Remove the specified form field – the method is RemoveField, not DeleteField
                editor.RemoveField(fieldName);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Field '{fieldName}' removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
