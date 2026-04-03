using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // System.Drawing.Color is required for background color

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "MyTextField"; // replace with the actual field name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF with FormEditor
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Configure visual attributes for the field
            formEditor.Facade = new FormFieldFacade();
            formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray; // set background to light gray

            // Apply the visual changes to the specific text field
            formEditor.DecorateField(fieldName);
            // To apply to all text fields, use:
            // formEditor.DecorateField(FieldType.Text);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with updated field background: {outputPdf}");
    }
}