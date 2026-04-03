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

        // Example input: decide whether the checkbox should be checked
        bool setChecked = true; // replace with actual input logic as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Name of the checkbox field in the PDF (adjust to your PDF's field name)
            string checkboxFieldName = "AgreeTerms";

            // Retrieve the field and cast to CheckboxField
            CheckboxField checkbox = form[checkboxFieldName] as CheckboxField;

            if (checkbox != null)
            {
                // Set the checkbox state based on input data
                checkbox.Checked = setChecked;
                // Alternatively, you can set the Value property:
                // checkbox.Value = setChecked ? "On" : "Off";
            }
            else
            {
                Console.Error.WriteLine($"Checkbox field '{checkboxFieldName}' not found.");
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}