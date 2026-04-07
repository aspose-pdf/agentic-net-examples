using System;
using Aspose.Pdf.Facades;               // FormEditor, FormFieldFacade, FieldType
using Aspose.Pdf.Text;                  // FontRepository (if needed)

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use FormEditor (a Facades API) to modify form field appearances
        using (Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPdf);

            // Create a facade that defines visual attributes for fields
            Aspose.Pdf.Facades.FormFieldFacade facade = new Aspose.Pdf.Facades.FormFieldFacade();

            // Set the custom font name (must be installed or embedded) and desired size
            // For non‑standard fonts use the CustomFont property; for standard 14 fonts the Font property works.
            facade.CustomFont = "Arial";   // replace with your font name
            facade.FontSize   = 12;        // desired font size

            // Assign the facade to the editor
            formEditor.Facade = facade;

            // Apply the visual attributes to all text fields in the form
            formEditor.DecorateField(Aspose.Pdf.Facades.FieldType.Text);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Custom font applied and saved to '{outputPdf}'.");
    }
}