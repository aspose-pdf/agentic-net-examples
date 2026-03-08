using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "styled_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Configure visual styling via FormFieldFacade
            formEditor.Facade = new FormFieldFacade();
            formEditor.Facade.BackgroundColor = System.Drawing.Color.Red;    // Background color
            formEditor.Facade.TextColor       = System.Drawing.Color.Blue;   // Text color
            formEditor.Facade.BorderColor     = System.Drawing.Color.Green;  // Border color
            formEditor.Facade.Alignment       = FormFieldFacade.AlignCenter; // Alignment

            // Apply the styling to all fields of the specified type (e.g., Text fields)
            formEditor.DecorateField(FieldType.Text);

            // Save the modified PDF to the output path
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPdf}'.");
    }
}
