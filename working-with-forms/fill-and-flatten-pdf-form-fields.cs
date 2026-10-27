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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                var form = doc.Form;
                if (form != null && form.Count > 0)
                {
                    // Fill a text box field named "Name"
                    if (form["Name"] is TextBoxField nameField)
                    {
                        nameField.Value = "John Doe";
                    }

                    // Fill a check box field named "Agree"
                    if (form["Agree"] is CheckboxField agreeField)
                    {
                        agreeField.Checked = true;
                    }
                }

                // Flatten the form so fields become non‑editable content
                form?.Flatten();

                // Save the flattened PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
