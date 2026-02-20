using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect input PDF path and output PDF path as command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PrintPdfForms <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF to the Form facade
            using (Form pdfForm = new Form())
            {
                pdfForm.BindPdf(inputPath);

                // List all form field names (optional, for debugging)
                Console.WriteLine("Form fields detected:");
                foreach (string fieldName in pdfForm.FieldNames)
                {
                    Console.WriteLine($" - {fieldName}");
                }

                // ----- Fill fields (replace field names with those present in your PDF) -----

                // Text field
                const string textField = "TextBox1";
                if (Array.Exists(pdfForm.FieldNames, f => f == textField))
                {
                    pdfForm.FillField(textField, "Sample Text");
                }

                // Check box (true = checked, false = unchecked)
                const string checkBoxField = "CheckBox1";
                if (Array.Exists(pdfForm.FieldNames, f => f == checkBoxField))
                {
                    pdfForm.FillField(checkBoxField, true);
                }

                // Radio button group (index of the option to select, zero‑based)
                const string radioField = "RadioGroup1";
                if (Array.Exists(pdfForm.FieldNames, f => f == radioField))
                {
                    pdfForm.FillField(radioField, 0); // selects the first option
                }

                // Combo box (value must match one of the defined items)
                const string comboField = "ComboBox1";
                if (Array.Exists(pdfForm.FieldNames, f => f == comboField))
                {
                    pdfForm.FillField(comboField, "Option2");
                }

                // Flatten all fields so they become part of the page content
                pdfForm.FlattenAllFields();

                // Save the modified PDF (uses the document‑save rule)
                pdfForm.Save(outputPath);
            }

            Console.WriteLine($"Document successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}