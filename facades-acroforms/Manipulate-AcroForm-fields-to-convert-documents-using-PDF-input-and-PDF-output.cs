using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing AcroForm fields
        const string inputPdf  = "input_form.pdf";
        // Output PDF after manipulation
        const string outputPdf = "output_filled.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF.
            // The Form class implements IDisposable, so we wrap it in a using block.
            using (Form form = new Form(inputPdf))
            {
                // Optional: list all field names present in the document.
                Console.WriteLine("AcroForm fields found:");
                foreach (string fieldName in form.FieldNames)
                {
                    Console.WriteLine($" - {fieldName}");
                }

                // Fill a text field (replace with the actual field name from your PDF).
                form.FillField("FirstName", "John");
                form.FillField("LastName",  "Doe");

                // Fill a check box field (true = checked, false = unchecked).
                form.FillField("SubscribeNewsletter", true);

                // Fill a radio button or list box field by index (zero‑based index of the option).
                // Example: select the second option (index 1) for a gender field.
                form.FillField("Gender", 1);

                // Flatten all fields so that the filled values become part of the page content
                // and the form fields are removed from the final PDF.
                form.FlattenAllFields();

                // Save the modified document to the specified output path.
                form.Save(outputPdf);
            }

            Console.WriteLine($"AcroForm processing completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during form manipulation: {ex.Message}");
        }
    }
}