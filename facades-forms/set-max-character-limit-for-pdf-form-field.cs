using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "registration_form.pdf";
        const string outputPath = "limited_username.pdf";
        const string fieldName = "Username";
        const int maxLength = 20;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Use FormEditor facade to modify the form field
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);
                // Correct method to set a character limit on a field
                editor.SetFieldLimit(fieldName, maxLength);
                editor.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with limited field to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
