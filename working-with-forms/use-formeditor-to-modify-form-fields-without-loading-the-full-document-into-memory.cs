using System;
using Aspose.Pdf.Facades;          // FormEditor, FieldType, PropertyFlag

class Program
{
    static void Main()
    {
        // Paths to the source PDF (with an existing AcroForm) and the output PDF.
        const string inputPdf  = "input_form.pdf";
        const string outputPdf = "output_form_modified.pdf";

        // Ensure the source file exists.
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // FormEditor works directly on the PDF file without loading the whole document into memory.
        // It implements IDisposable, so we wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF to the editor.
            editor.BindPdf(inputPdf);

            // -----------------------------------------------------------------
            // 1. Add a new single‑line text field.
            //    Parameters: field type, field name, page number (1‑based),
            //    lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            // -----------------------------------------------------------------
            bool added = editor.AddField(FieldType.Text, "NewTextField", 1,
                                         100f, 500f, 300f, 520f);
            if (!added)
                Console.WriteLine("Failed to add NewTextField.");

            // -----------------------------------------------------------------
            // 2. Set a maximum character limit for the newly added text field.
            // -----------------------------------------------------------------
            editor.SetFieldLimit("NewTextField", 30); // limit to 30 characters

            // -----------------------------------------------------------------
            // 3. Mark the field as required.
            // -----------------------------------------------------------------
            editor.SetFieldAttribute("NewTextField", PropertyFlag.Required);

            // -----------------------------------------------------------------
            // 4. Rename an existing field (if it exists).
            // -----------------------------------------------------------------
            // Note: RenameField throws if the source name is not found; we can ignore the result.
            try
            {
                editor.RenameField("OldFieldName", "RenamedField");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Rename failed (field may not exist): {ex.Message}");
            }

            // -----------------------------------------------------------------
            // 5. Remove an unwanted field.
            // -----------------------------------------------------------------
            try
            {
                editor.RemoveField("ObsoleteField");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove failed (field may not exist): {ex.Message}");
            }

            // -----------------------------------------------------------------
            // 6. Change visual appearance of all text fields (optional).
            //    Here we set a light gray background and center alignment.
            // -----------------------------------------------------------------
            editor.Facade = new FormFieldFacade(); // reset visual facade
            editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;
            editor.Facade.Alignment = FormFieldFacade.AlignCenter;
            editor.DecorateField(FieldType.Text); // apply to all text fields

            // -----------------------------------------------------------------
            // 7. Save the modified PDF to a new file.
            // -----------------------------------------------------------------
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Form fields modified and saved to '{outputPdf}'.");
    }
}