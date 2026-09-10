using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // ----- Text field "Name" -----
                if (doc.Form["Name"] is TextBoxField nameField)
                {
                    // For TextBoxField the value is set via the Value property
                    nameField.Value = "John Doe";
                }

                // ----- Checkbox field "Agree" -----
                if (doc.Form["Agree"] is CheckboxField agreeField)
                {
                    // For CheckboxField the checked state is set via the Checked property
                    agreeField.Checked = true;
                }
            }

            // Flatten the form – removes fields and places their values directly on the page
            doc.Form?.Flatten();

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
