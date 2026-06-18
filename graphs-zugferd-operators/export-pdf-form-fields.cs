using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF containing a single text box form field.
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            Rectangle fieldRect = new Rectangle(100, 600, 300, 620);
            TextBoxField nameField = new TextBoxField(createDoc.Pages[1], fieldRect);
            nameField.PartialName = "FullName";
            nameField.Value = "John Doe";
            createDoc.Form.Add(nameField);
            createDoc.Save("input.pdf");
        }

        // Step 2: Open the PDF and export its form fields to JSON.
        using (Document doc = new Document("input.pdf"))
        {
            // Collect field names and values.
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();
            foreach (Aspose.Pdf.Forms.Field field in doc.Form.Fields)
            {
                if (field is TextBoxField txtField)
                {
                    fieldValues.Add(txtField.PartialName, txtField.Value);
                }
                else if (field is CheckboxField chkField)
                {
                    fieldValues.Add(chkField.PartialName, chkField.Checked ? "true" : "false");
                }
                else if (field is RadioButtonField radioField)
                {
                    // RadioButtonField.Selected returns an int (index of selected option).
                    // Treat any non‑negative value as "selected".
                    bool isSelected = radioField.Selected >= 0;
                    fieldValues.Add(radioField.PartialName, isSelected ? "true" : "false");
                }
                else if (field is ComboBoxField comboField)
                {
                    fieldValues.Add(comboField.PartialName, comboField.Value);
                }
                // Add other field types as needed, respecting the four‑element collection limit.
            }

            // Serialize the dictionary to JSON.
            string json = JsonSerializer.Serialize(fieldValues, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }
    }
}