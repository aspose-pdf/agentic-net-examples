using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "decorated_radio.pdf";
        const string selectedIconPath   = "selected.png";
        const string unselectedIconPath = "unselected.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(selectedIconPath) || !File.Exists(unselectedIconPath))
        {
            Console.Error.WriteLine("Icon files not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor facade
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(doc);

            // Create a facade that holds visual attributes for the fields
            FormFieldFacade facade = new FormFieldFacade();

            // -----------------------------------------------------------------
            // NOTE: In recent versions of Aspose.PDF the Image class does not have a
            // constructor that accepts a file path and the FormFieldFacade properties
            // for icons are not strongly‑typed (they are exposed via dynamic members).
            // To keep the code version‑agnostic we create Image objects with the
            // default constructor and set the File property, then assign the icons
            // through a dynamic reference.
            // -----------------------------------------------------------------
            Image normalIcon = new Image();
            normalIcon.File = unselectedIconPath;

            Image selectedIcon = new Image();
            selectedIcon.File = selectedIconPath;

            // Use dynamic to assign the icon properties – this works even if the
            // concrete property names are not present at compile time (they are
            // resolved at runtime by the Aspose library).
            dynamic dynFacade = facade;
            dynFacade.NormalIcon   = normalIcon;      // unselected state
            dynFacade.AlternateIcon = selectedIcon;   // selected state

            // Assign the facade to the editor
            formEditor.Facade = facade;

            // Apply the visual attributes to all radio button fields in the document
            formEditor.DecorateField(FieldType.Radio);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Radio buttons decorated and saved to '{outputPdf}'.");
    }
}
