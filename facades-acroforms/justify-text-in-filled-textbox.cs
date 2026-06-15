using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with a text box field
        const string outputPdf = "output.pdf";     // result PDF
        const string fieldName = "TextBox1";       // fully qualified name of the text box field
        const string fieldValue = "Justified text inside the field.";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Fill the text box field with the desired value
            using (Form form = new Form(doc))
            {
                form.FillField(fieldName, fieldValue);
            }

            // Apply justified alignment to the filled field
            using (FormEditor editor = new FormEditor(doc))
            {
                // Configure visual attributes via FormFieldFacade
                editor.Facade = new FormFieldFacade
                {
                    Alignment = FormFieldFacade.AlignJustified
                };

                // Apply the facade settings to the specific field
                editor.DecorateField(fieldName);

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with justified text field: {outputPdf}");
    }
}