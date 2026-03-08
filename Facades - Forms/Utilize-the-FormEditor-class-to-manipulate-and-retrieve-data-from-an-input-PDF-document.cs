using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use FormEditor to work with form fields.
        // The class implements IDisposable, so wrap it in a using block.
        using (Aspose.Pdf.Facades.FormEditor editor = new Aspose.Pdf.Facades.FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // -----------------------------------------------------------------
            // 1. Remove an existing field (if it exists).
            // -----------------------------------------------------------------
            // No exception is thrown if the field name is not present.
            editor.RemoveField("OldField");

            // -----------------------------------------------------------------
            // 2. Move an existing field to a new location.
            //    Coordinates are in points; the page number is 1‑based.
            // -----------------------------------------------------------------
            // Example: move field "Address" to a rectangle (50,500)-(250,520) on page 1.
            editor.MoveField("Address", 50f, 500f, 250f, 520f);

            // -----------------------------------------------------------------
            // 3. Add a new text field to page 1.
            // -----------------------------------------------------------------
            // Parameters: field type, field name, page number, llx, lly, urx, ury.
            editor.AddField(Aspose.Pdf.Facades.FieldType.Text,
                            "NewTextField",
                            1,
                            100f, 100f,   // lower‑left corner
                            300f, 120f);  // upper‑right corner

            // -----------------------------------------------------------------
            // 4. Set visual attributes for the newly added field.
            //    Only alignment is set here to avoid System.Drawing.Color usage.
            // -----------------------------------------------------------------
            editor.Facade = new Aspose.Pdf.Facades.FormFieldFacade();
            editor.Facade.Alignment = Aspose.Pdf.Facades.FormFieldFacade.AlignCenter;
            editor.DecorateField("NewTextField");

            // -----------------------------------------------------------------
            // 5. Retrieve appearance flags of a field.
            // -----------------------------------------------------------------
            Aspose.Pdf.Annotations.AnnotationFlags appearanceFlags =
                editor.GetFieldAppearance("NewTextField");
            Console.WriteLine($"Appearance flags for 'NewTextField': {appearanceFlags}");

            // -----------------------------------------------------------------
            // 6. Save the modified PDF to a new file.
            // -----------------------------------------------------------------
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Form manipulation completed. Output saved to '{outputPdf}'.");
    }
}