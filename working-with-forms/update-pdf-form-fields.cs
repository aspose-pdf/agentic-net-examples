using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form_input.pdf";
        const string outputPdf = "form_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (core API, no Facades).
        Document doc = new Document(inputPdf);

        // Set a text field value.
        SetTextFieldValue(doc, "CustomerName", "John Doe");

        // Set a checkbox value.
        SetCheckBoxValue(doc, "AgreeTerms", true);

        // Set a radio‑button option.
        SetRadioButtonValue(doc, "Gender", "Male");

        // Save the modified PDF.
        doc.Save(outputPdf);
        Console.WriteLine($"Form fields updated and saved to '{outputPdf}'.");
    }

    // Helper to set a TextBox field.
    static void SetTextFieldValue(Document doc, string fieldName, string value)
    {
        if (doc.Form[fieldName] is TextBoxField txt)
        {
            txt.Value = value;
        }
        else
        {
            Console.Error.WriteLine($"Text field '{fieldName}' not found or not a TextBoxField.");
        }
    }

    // Helper to set a CheckBox field using runtime type inspection.
    static void SetCheckBoxValue(Document doc, string fieldName, bool check)
    {
        var field = doc.Form[fieldName];
        if (field == null)
        {
            Console.Error.WriteLine($"Check box '{fieldName}' not found.");
            return;
        }

        // TextBoxField is handled separately above; here we handle CheckboxField via reflection/dynamic.
        if (field.GetType().Name == "CheckboxField")
        {
            try
            {
                dynamic chk = field;
                chk.Checked = check;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to set Checked property on '{fieldName}': {ex.Message}");
            }
        }
        else
        {
            Console.Error.WriteLine($"Field '{fieldName}' is not a CheckboxField.");
        }
    }

    // Helper to select a radio‑button option using runtime inspection.
    static void SetRadioButtonValue(Document doc, string groupName, string optionValue)
    {
        bool found = false;
        foreach (var field in doc.Form)
        {
            if (field.GetType().Name != "RadioButtonOptionField")
                continue;

            // Retrieve PartialName (group name) and Option (value) via reflection.
            var partialNameProp = field.GetType().GetProperty("PartialName");
            var optionProp = field.GetType().GetProperty("Option");
            if (partialNameProp == null || optionProp == null)
                continue;

            string partialName = partialNameProp.GetValue(field) as string;
            string option = optionProp.GetValue(field) as string;

            if (partialName == groupName && option == optionValue)
            {
                // Set the Selected property via reflection/dynamic.
                var selectedProp = field.GetType().GetProperty("Selected");
                if (selectedProp != null && selectedProp.CanWrite)
                {
                    selectedProp.SetValue(field, true);
                    found = true;
                    break;
                }
                else
                {
                    // Fallback to dynamic if property exists at runtime.
                    try
                    {
                        dynamic dyn = field;
                        dyn.Selected = true;
                        found = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Unable to set Selected on radio option: {ex.Message}");
                    }
                }
            }
        }
        if (!found)
        {
            Console.Error.WriteLine($"Radio button option '{optionValue}' for group '{groupName}' not found.");
        }
    }
}
