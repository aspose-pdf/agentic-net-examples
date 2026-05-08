using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source PDF exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor to modify the appearance of a specific form field
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Create a facade that defines visual attributes
            FormFieldFacade facade = new FormFieldFacade();

            // Custom border color
            facade.BorderColor = System.Drawing.Color.Green;

            // Custom background shade
            facade.BackgroundColor = System.Drawing.Color.LightGray;

            // Font style: use a non‑standard font and size
            facade.CustomFont = "Helvetica";   // name of the font
            facade.FontSize   = 12;            // point size

            // Assign the facade to the editor
            editor.Facade = facade;

            // Apply the styling to the field named "CustomerName"
            editor.DecorateField("CustomerName");

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"CustomerName\" decorated and saved to '{outputPdf}'.");
    }
}