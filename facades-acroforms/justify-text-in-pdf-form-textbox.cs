using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";               // source PDF with a textbox form field
        const string outputPath = "justified_output.pdf";   // result PDF
        const string fieldName = "TextBox1";                // name of the textbox field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the PDF and fill the textbox field with the desired text
            // ------------------------------------------------------------
            Document doc = new Document(inputPath);
            if (doc.Form[fieldName] is TextBoxField txtField)
            {
                string sampleText = "This is a sample paragraph that will be justified inside the textbox field of the PDF form. It demonstrates how to apply justified alignment using FormEditor.";
                txtField.Value = sampleText;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField or does not exist.");
                return;
            }

            // Save the filled PDF to a temporary file (required because FormEditor works on a file or a Document instance)
            string tempFilledPath = Path.GetTempFileName();
            doc.Save(tempFilledPath);

            // ------------------------------------------------------------
            // 2. Apply justified alignment to the filled textbox field
            // ------------------------------------------------------------
            using (Document filledDoc = new Document(tempFilledPath))
            {
                // FormEditor can be constructed with a Document instance
                FormEditor editor = new FormEditor(filledDoc);

                // The FormEditor does not expose an alignment overload on DecorateField.
                // Instead, we configure a FormFieldFacade with the desired alignment and assign it to the editor.
                editor.Facade = new FormFieldFacade();
                editor.Facade.Alignment = FormFieldFacade.AlignJustified; // Justified alignment

                // DecorateField will apply the appearance changes (including the alignment) to the field.
                editor.DecorateField(fieldName);

                // Save the final PDF with justified text
                editor.Save(outputPath);
            }

            Console.WriteLine($"Justified textbox saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
