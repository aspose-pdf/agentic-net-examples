using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "CheckBox1"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document doc = new Document(inputPath))
            {
                // Try to get the checkbox field by name
                CheckboxField checkbox = doc.Form[fieldName] as CheckboxField;

                // If not found, fall back to the first checkbox field in the form (if any)
                if (checkbox == null)
                {
                    var fields = doc.Form?.Fields;
                    if (fields != null && fields.Count() > 0)
                    {
                        // Retrieve the first field and check its type safely
                        var firstField = fields[0];
                        if (firstField is CheckboxField cbField)
                        {
                            checkbox = cbField;
                        }
                    }
                }

                if (checkbox != null)
                {
                    // Assign the boolean value (true = checked, false = unchecked)
                    checkbox.Checked = true;
                    // Alternative way:
                    // checkbox.Value = "On"; // or "Off" to uncheck
                }
                else
                {
                    Console.Error.WriteLine("Checkbox field not found.");
                }

                // Save the modified PDF (lifecycle: save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Checkbox state updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
