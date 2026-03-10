using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string srcPdf = "template.pdf";
        const string destPdf = "filled.pdf";

        if (!File.Exists(srcPdf))
        {
            Console.Error.WriteLine($"Source file not found: {srcPdf}");
            return;
        }

        // Load the PDF form using the Form facade
        using (Form form = new Form(srcPdf))
        {
            // Populate text fields with validation and formatting
            ValidateAndFill(form, "FirstName", "John");
            ValidateAndFill(form, "LastName", "Doe");

            // Example: fill a date field with a specific format
            string birthDate = DateTime.Now.AddYears(-30).ToString("MM/dd/yyyy");
            ValidateAndFill(form, "BirthDate", birthDate);

            // Example: set a checkbox field
            ValidateAndCheck(form, "SubscribeNewsletter", true);

            // Example: select a radio button option by its export value
            // The export value must match the value defined in the PDF form for the desired option
            ValidateAndSelectRadio(form, "Gender", "Female"); // e.g., "Male" or "Female"

            // Save the updated PDF
            form.Save(destPdf);
        }

        Console.WriteLine($"Form filled and saved to '{destPdf}'.");
    }

    // Helper to fill a text field after confirming its existence and type
    static void ValidateAndFill(Form form, string fieldName, string value)
    {
        if (Array.Exists(form.FieldNames, name => name.Equals(fieldName, StringComparison.Ordinal)))
        {
            var fieldType = form.GetFieldType(fieldName);
            if (fieldType == FieldType.Text)
            {
                // FitFontSize = true to avoid overflow
                bool filled = form.FillField(fieldName, value, true);
                if (!filled)
                    Console.Error.WriteLine($"Failed to fill field '{fieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a text field (type={fieldType}).");
            }
        }
        else
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found in the form.");
        }
    }

    // Helper to set a checkbox field after validation
    static void ValidateAndCheck(Form form, string fieldName, bool check)
    {
        if (Array.Exists(form.FieldNames, name => name.Equals(fieldName, StringComparison.Ordinal)))
        {
            var fieldType = form.GetFieldType(fieldName);
            if (fieldType == FieldType.CheckBox)
            {
                bool filled = form.FillField(fieldName, check);
                if (!filled)
                    Console.Error.WriteLine($"Failed to set checkbox '{fieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a checkbox (type={fieldType}).");
            }
        }
        else
        {
            Console.Error.WriteLine($"Checkbox field '{fieldName}' not found.");
        }
    }

    // Helper to select a radio button option by its export value after validation
    static void ValidateAndSelectRadio(Form form, string fieldName, string exportValue)
    {
        if (Array.Exists(form.FieldNames, name => name.Equals(fieldName, StringComparison.Ordinal)))
        {
            // In Aspose.PDF, radio button groups are reported as CheckBox fields.
            var fieldType = form.GetFieldType(fieldName);
            if (fieldType == FieldType.CheckBox)
            {
                // For radio buttons the FillField overload expects the export value of the option.
                bool filled = form.FillField(fieldName, exportValue);
                if (!filled)
                    Console.Error.WriteLine($"Failed to select radio option '{fieldName}' with value '{exportValue}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a radio button (type={fieldType}).");
            }
        }
        else
        {
            Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
        }
    }
}
