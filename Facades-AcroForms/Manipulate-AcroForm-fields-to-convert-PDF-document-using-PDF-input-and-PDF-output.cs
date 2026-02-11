using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the AcroForm object
            Form form = pdfDocument.Form;

            // Improve performance when filling many fields
            form.AutoRecalculate = false;

            // Iterate over all form fields and set sample values where possible
            foreach (Field field in form)
            {
                // Text box fields – set a sample string
                if (field is TextBoxField textBox)
                {
                    textBox.Value = "Sample";
                }
                // For other field types (CheckBox, RadioButton, ListBox, etc.)
                // we avoid using type‑specific APIs that may not be present in the
                // current Aspose.Pdf version. Instead, we set a generic value when
                // the field supports it.
                else if (!string.IsNullOrEmpty(field.Value))
                {
                    // Attempt to set a generic "On" value – many form fields accept it.
                    field.Value = "On";
                }
            }

            // Flatten the form to make fields part of the static content.
            // The overload that accepts FlattenSettings is not available in all
            // versions, so we use the parameter‑less method which works universally.
            pdfDocument.Form.Flatten();

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
