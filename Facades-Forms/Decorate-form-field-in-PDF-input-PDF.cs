using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with AcroForm
        const string outputPdf = "decorated_output.pdf"; // result PDF
        const string fieldName = "myTextField";        // fully‑qualified name of the field to decorate

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialise the FormEditor facade and bind the source PDF.
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPdf);

                // Create a FormFieldFacade to specify visual attributes.
                // Only cross‑platform properties are set (avoid System.Drawing.Color).
                editor.Facade = new FormFieldFacade
                {
                    // Example: centre‑align the text inside the field.
                    Alignment = FormFieldFacade.AlignCenter
                };

                // Apply the visual changes to the specified field.
                editor.DecorateField(fieldName);

                // Save the modified document.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Form field '{fieldName}' decorated successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}