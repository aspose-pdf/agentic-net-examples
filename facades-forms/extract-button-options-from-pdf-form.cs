using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";            // source PDF with form fields
        const string outputTxtPath = "button_options.txt"; // file to store the results

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        var document = new Document(inputPdfPath);

        // Ensure the document actually contains a form
        if (document.Form == null || document.Form.Fields == null)
        {
            Console.WriteLine("No form fields found in the PDF.");
            return;
        }

        // Open the output text file
        using (var writer = new StreamWriter(outputTxtPath, false))
        {
            // ChoiceField is the base class for fields that expose selectable options
            // (ListBoxField, ComboBoxField, RadioButtonField). ButtonField (push button) does not have options.
            var choiceFields = document.Form.Fields
                                         .OfType<ChoiceField>()
                                         .Where(f => f.Options != null && f.Options.Count > 0);

            foreach (var field in choiceFields)
            {
                // Write the field name (FullName falls back to PartialName)
                writer.WriteLine($"Field: {field.FullName ?? field.PartialName}");
                writer.WriteLine("Options:");

                // Each option is an Aspose.Pdf.Forms.Option object exposing Name (display text) and Value (export value)
                foreach (var opt in field.Options)
                {
                    writer.WriteLine($"  - {opt.Name} (value: {opt.Value})");
                }

                writer.WriteLine(); // blank line between fields
            }
        }

        Console.WriteLine($"Button option values have been written to '{outputTxtPath}'.");
    }
}
