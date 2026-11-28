using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Comments"; // name of the multiline textbox field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable, so wrap it in a using block
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Set the maximum character limit for the specified textbox field
                editor.SetFieldLimit(fieldName, 200);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}