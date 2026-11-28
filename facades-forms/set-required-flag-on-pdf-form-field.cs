using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "draft_form_v2.pdf";
        const string outputPath = "required_phone.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable, so wrap it in a using block
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF containing the form
                editor.BindPdf(inputPath);

                // Set the Required flag on the "PhoneNumber" field
                // The correct method is SetFieldAttribute, not SetFieldProperty
                editor.SetFieldAttribute("PhoneNumber", PropertyFlag.Required);

                // Save the updated PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Saved required phone PDF to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
