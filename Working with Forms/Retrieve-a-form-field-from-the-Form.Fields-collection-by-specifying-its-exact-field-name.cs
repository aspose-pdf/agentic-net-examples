using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // Path to the PDF containing the form
        const string fieldName = "myTextField";    // Exact name of the field to retrieve

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check that the document actually contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("The document does not contain any form fields.");
                return;
            }

            // Retrieve the field by its exact name.
            // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Output basic information about the retrieved field
            Console.WriteLine($"Field '{fieldName}' found. Type: {field.GetType().Name}");

            // Demonstrate how to access the value for common field types using runtime inspection.
            string typeName = field.GetType().Name;

            if (typeName == "TextBoxField")
            {
                // In many Aspose.Pdf versions the text value is exposed via the 'Value' property.
                dynamic txt = field;
                Console.WriteLine($"Current text value: {txt.Value}");
            }
            else if (typeName == "CheckBoxField")
            {
                dynamic chk = field;
                Console.WriteLine($"Checked state: {chk.Checked}");
            }
            else if (typeName == "RadioButtonOptionField" || typeName == "RadioButtonField")
            {
                dynamic rad = field;
                // Radio button option fields expose the selected value via the 'Value' property.
                Console.WriteLine($"Selected value: {rad.Value}");
            }
            else
            {
                Console.WriteLine("Field type handling not implemented for this field.");
            }
        }
    }
}
