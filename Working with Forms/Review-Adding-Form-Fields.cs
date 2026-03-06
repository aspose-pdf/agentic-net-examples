using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // existing PDF to which fields will be added
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a FormEditor instance bound to the existing PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Add a single‑line text field
            // Parameters: field type, field name, page number (1‑based), lower‑left X/Y, upper‑right X/Y
            bool textAdded = formEditor.AddField(FieldType.Text, "CustomerName", 1, 100f, 500f, 300f, 520f);
            if (!textAdded)
                Console.Error.WriteLine("Failed to add text field.");

            // Add a checkbox field
            bool checkAdded = formEditor.AddField(FieldType.CheckBox, "SubscribeNewsletter", 1, 100f, 550f, 120f, 570f);
            if (!checkAdded)
                Console.Error.WriteLine("Failed to add checkbox field.");

            // Optional: set visual appearance for all fields (background, text, border colors)
            // FormFieldFacade uses System.Drawing.Color, but to stay cross‑platform we omit color settings
            // and rely on default appearance.

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Form fields added and saved to '{outputPdf}'.");
    }
}