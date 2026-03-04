using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // source PDF with AcroForm fields
        const string outputPdf = "filled_form.pdf";  // result PDF after filling

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF.
            // The constructor loads the document internally.
            using (Form form = new Form(inputPdf))
            {
                // Example: fill a text field.
                form.FillField("FirstName", "John");

                // Example: fill another text field.
                form.FillField("LastName", "Smith");

                // Example: check a checkbox field.
                form.FillField("SubscribeNewsletter", true);

                // Example: select the third item (index = 2) in a combo box.
                form.FillField("CountryCombo", 2);

                // Example: fill a radio button group by index.
                form.FillField("GenderRadio", 1); // 0‑based index of the option

                // Optional: list all field names (useful for debugging).
                Console.WriteLine("Form fields:");
                foreach (string fieldName in form.FieldNames)
                {
                    Console.WriteLine($" - {fieldName}");
                }

                // Save the modified PDF to the output path.
                form.Save(outputPdf);
            }

            Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing form: {ex.Message}");
        }
    }
}