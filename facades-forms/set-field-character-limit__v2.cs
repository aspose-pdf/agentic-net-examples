using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "order_form.pdf";
        const string outputPath = "limited_code.pdf";
        const string fieldName  = "ProductCode";
        const int    maxLength  = 15;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable, so wrap it in a using block.
            using (FormEditor editor = new FormEditor())
            {
                // Bind the existing PDF document.
                editor.BindPdf(inputPath);

                // Set the maximum character limit for the specified form field.
                // The correct FormEditor method is SetFieldLimit(name, maxLength).
                editor.SetFieldLimit(fieldName, maxLength);

                // Save the modified PDF to a new file.
                editor.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with field limit to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
