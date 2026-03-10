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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the form fields
        using (Document doc = new Document(inputPath))
        {
            // ----- TextBoxField -----
            // Access the text box by its full field name (replace with actual name)
            // The Form indexer returns a generic Field, so cast to TextBoxField
            if (doc.Form["CustomerName"] is TextBoxField txtField)
            {
                txtField.Value = "John Doe"; // Assign the desired text value
            }

            // ----- CheckBoxField -----
            // Access the checkbox field (replace with actual name)
            if (doc.Form["SubscribeNewsletter"] is CheckboxField chkField)
            {
                chkField.Checked = true; // Mark the checkbox as checked
            }

            // ----- RadioButtonField -----
            // Access the radio button group (replace with actual name)
            if (doc.Form["PaymentMethod"] is RadioButtonField radioField)
            {
                // Option 1: select by index (1‑based). Here we select the second option.
                radioField.Selected = 2;

                // Option 2: select by export value (uncomment if you prefer)
                // radioField.Value = "CreditCard";
            }

            // Persist the changes to a new PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields updated and saved to '{outputPath}'.");
    }
}