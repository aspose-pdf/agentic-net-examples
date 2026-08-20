using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "ObsoleteField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Use FormEditor to edit the PDF form
            using (FormEditor editor = new FormEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPath);

                // Remove the unwanted field
                editor.RemoveField(fieldName);

                // Save the result to a new file
                editor.Save(outputPath);
            }

            Console.WriteLine($"Field \"{fieldName}\" removed. Output saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}