using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_form.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the form object
                Form form = doc.Form;

                // Check if the document actually contains form fields
                if (form != null && form.Count > 0)
                {
                    // Example: set the value of a text box field named "Name"
                    if (form.HasField("Name"))
                    {
                        // Cast to the specific field type to access the Value property
                        TextBoxField nameField = form["Name"] as TextBoxField;
                        if (nameField != null)
                        {
                            nameField.Value = "John Doe";
                        }
                    }

                    // Example: check a checkbox field named "Agree"
                    if (form.HasField("Agree"))
                    {
                        // Retrieve the field as a generic object. The concrete type may be
                        // CheckBoxField, RadioButtonOptionField, etc., which can vary between
                        // Aspose.Pdf versions. To stay compatible we avoid compile‑time references
                        // to those concrete types and use runtime inspection.
                        var agreeWidget = form["Agree"];
                        if (agreeWidget != null)
                        {
                            // Strong‑typed handling for known stable types (e.g., TextBoxField)
                            if (agreeWidget is TextBoxField tb)
                            {
                                tb.Value = "true"; // just an example
                            }
                            // Fallback handling for checkbox‑like fields using reflection/dynamic
                            else if (agreeWidget.GetType().Name == "CheckBoxField" ||
                                     agreeWidget.GetType().Name == "RadioButtonOptionField")
                            {
                                // Using dynamic bypasses the need for a compile‑time reference.
                                // The property "Checked" exists on both checkbox and radio button
                                // option field types in the versions that expose them.
                                dynamic dynField = agreeWidget;
                                dynField.Checked = true;
                            }
                        }
                    }

                    // If you need to make the form fields non‑interactive, uncomment:
                    // form.Flatten();
                }
                else
                {
                    Console.WriteLine("No form fields found in the document.");
                }

                // Persist the changes by saving the document to a new file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form modifications saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
