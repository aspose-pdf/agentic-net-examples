using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextBox"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF, set justification, and save
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Configure visual attributes: set alignment to justified
            editor.Facade = new FormFieldFacade();
            editor.Facade.Alignment = FormFieldFacade.AlignJustified;

            // Apply the alignment to the specified textbox field
            editor.DecorateField(fieldName);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Justified PDF saved to '{outputPath}'.");
    }
}