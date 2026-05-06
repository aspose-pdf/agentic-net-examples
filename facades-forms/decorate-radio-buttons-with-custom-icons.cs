using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with radio button fields
        const string outputPdf = "output.pdf";         // PDF after decoration
        const string selectedIconPath   = "selected.png";   // icon for the checked state
        const string unselectedIconPath = "unselected.png"; // icon for the unchecked state

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the form to enumerate field names and determine their types.
        using (Form form = new Form(inputPdf))
        {
            // Bind the same document to a FormEditor – this object will apply visual changes.
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPdf);

                // Iterate over every field in the form.
                foreach (string fieldName in form.FieldNames)
                {
                    // Process only radio button fields.
                    if (form.GetFieldType(fieldName) == FieldType.Radio)
                    {
                        // Create a new facade that describes the visual appearance.
                        FormFieldFacade facade = new FormFieldFacade();

                        // -----------------------------------------------------------------
                        // NOTE: FormFieldFacade provides properties for setting custom
                        // icons for check‑box and radio‑button fields. The exact property
                        // names may vary between library versions (e.g. NormalIcon,
                        // AlternateIcon, SelectedIcon, UnselectedIcon). Replace the
                        // placeholders below with the correct property names for your
                        // Aspose.Pdf version.
                        // -----------------------------------------------------------------
                        // Example (if the properties exist):
                        // facade.NormalIcon   = selectedIconPath;   // displayed when selected
                        // facade.AlternateIcon = unselectedIconPath; // displayed when not selected

                        // If the library version uses different property names, adjust
                        // accordingly. The key idea is to assign the image file paths
                        // that represent the selected and unselected states.

                        // Assign the prepared facade to the editor.
                        editor.Facade = facade;

                        // Apply the visual changes to the current radio button field.
                        editor.DecorateField(fieldName);
                    }
                }

                // Save the modified PDF.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Radio button fields decorated and saved to '{outputPdf}'.");
    }
}