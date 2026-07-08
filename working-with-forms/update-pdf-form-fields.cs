using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core API. This loads the whole document
        // into memory, which is the only supported way to edit form fields without
        // using the restricted Facades namespace.
        Document pdfDocument = new Document(inputPath);

        // ----- Text field -----
        // Retrieve the field by its name and cast to the appropriate type.
        if (pdfDocument.Form != null && pdfDocument.Form["CustomerName"] is TextBoxField nameField)
        {
            // In the core API the property to set the field value is "Value" (not "Text").
            nameField.Value = "John Doe"; // set the value for a text box
        }
        else
        {
            Console.Error.WriteLine("Text field 'CustomerName' not found or is not a TextBoxField.");
        }

        // ----- Checkbox field -----
        if (pdfDocument.Form != null && pdfDocument.Form["AgreeTerms"] is CheckboxField agreeField)
        {
            // The CheckboxField class exists in Aspose.Pdf.Forms and provides a "Checked" property.
            agreeField.Checked = true; // set the checkbox to checked
        }
        else
        {
            Console.Error.WriteLine("Checkbox field 'AgreeTerms' not found or is not a CheckboxField.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Form fields updated and saved to '{outputPath}'.");
    }
}
