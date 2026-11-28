using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "output112.pdf";
        const string sourceFieldName = "Quantity";
        const string copiedFieldName = "QuantityCopy"; // new name for the duplicated field

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // FormEditor is a Facade class used for form field manipulation.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(sourcePath);

            // Duplicate the definition of the field "Quantity".
            // CopyOuterField copies the whole field definition (including its widget).
            // The new field must have a distinct name.
            editor.CopyOuterField(sourceFieldName, copiedFieldName);

            // Move the newly created field to page 1 at the desired location.
            // MoveField takes the lower‑left (llx,lly) and upper‑right (urx,ury) coordinates.
            // Here we place the lower‑left corner at (200,200) and give the field a width of 100 and height of 30.
            editor.MoveField(copiedFieldName, 200f, 200f, 300f, 230f);

            // Configure the facade to paint a purple border.
            editor.Facade.BorderColor = System.Drawing.Color.Purple; // fully qualified to avoid ambiguity
            editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // thin border
            editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid;

            // Apply the decoration to the copied field.
            editor.DecorateField(copiedFieldName);

            // Save the modified PDF to the desired output file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{sourceFieldName}' copied to '{copiedFieldName}' and styled. Output saved to '{outputPath}'.");
    }
}
