using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF form
        using (Document doc = new Document(inputPath))
        {
            // ----- Set a TextBoxField value -----
            // Replace "TextBoxFieldName" with the actual field name in the PDF
            const string textBoxName = "TextBoxFieldName";
            if (doc.Form.HasField(textBoxName))
            {
                // The indexer returns a generic Field; cast to TextBoxField to access Value
                TextBoxField txtField = (TextBoxField)doc.Form[textBoxName];
                txtField.Value = "Sample text entered programmatically";
            }

            // ----- Check a CheckBoxField -----
            // Replace "CheckBoxFieldName" with the actual field name in the PDF
            const string checkBoxName = "CheckBoxFieldName";
            if (doc.Form.HasField(checkBoxName))
            {
                CheckboxField chkField = (CheckboxField)doc.Form[checkBoxName];
                chkField.Checked = true; // Marks the checkbox as checked
            }

            // ----- Select a RadioButtonField option -----
            // Replace "RadioButtonFieldName" with the actual field name in the PDF
            const string radioButtonName = "RadioButtonFieldName";
            if (doc.Form.HasField(radioButtonName))
            {
                RadioButtonField radioField = (RadioButtonField)doc.Form[radioButtonName];
                // Select the first option (indexing starts at 1)
                // Alternatively, set radioField.Value to the export value of the desired option
                radioField.Selected = 1;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }
}