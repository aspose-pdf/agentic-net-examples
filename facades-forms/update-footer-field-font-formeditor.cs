using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for PropertyFlag

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "FooterFont.pdf";
        const string fieldName = "Footer";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document into the FormEditor
                editor.BindPdf(inputPath);

                // Define the visual style for the field using a facade
                editor.Facade = new FormFieldFacade
                {
                    Font = Aspose.Pdf.Facades.FontStyle.TimesRoman, // Times New Roman
                    FontSize = 12,
                    TextColor = System.Drawing.Color.Blue // System.Drawing.Color is required here
                };

                // Apply the facade to the existing Footer field
                editor.DecorateField(fieldName);

                // Make the field read‑only (optional)
                editor.SetFieldAttribute(fieldName, PropertyFlag.ReadOnly);

                // Save the updated PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Footer field font updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
