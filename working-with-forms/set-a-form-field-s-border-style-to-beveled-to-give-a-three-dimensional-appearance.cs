using System;
using System.IO;
using Aspose.Pdf;                         // Core PDF classes
using Aspose.Pdf.Facades;                // FormEditor and related facades
using Aspose.Pdf.Facades;                // FormFieldFacade is also in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the form field
        const string outputPdf = "output_beveled.pdf";
        const string fieldName = "MyTextField";    // Name of the field to modify

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FormEditor bound to the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Prepare a facade that defines visual attributes
                FormFieldFacade facade = new FormFieldFacade();

                // Set the border style to beveled using the constant defined in FormFieldFacade
                facade.BorderStyle = FormFieldFacade.BorderStyleBeveled;

                // Assign the facade to the editor
                editor.Facade = facade;

                // Apply the visual changes to the specific field
                editor.DecorateField(fieldName);

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Form field '{fieldName}' border style set to beveled. Saved to '{outputPdf}'.");
    }
}