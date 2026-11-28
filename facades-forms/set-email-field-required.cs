using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "application_form_v6.pdf";
        const string outputPath = "required_email2.pdf";
        const string fieldName = "Email";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable; ensure deterministic disposal
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF into the editor
                editor.BindPdf(inputPath);

                // Mark the "Email" field as required using the correct API
                editor.SetFieldAttribute(fieldName, PropertyFlag.Required);

                // Save the updated PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with required Email field: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
