using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdf = "input_form.pdf";

        // Output PDF with modified form fields
        const string outputPdf = "output_form_filled.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document. The core API loads the document structure, but
            // it does not require the Facades wrappers and works with the Form object
            // directly, allowing us to modify fields without the heavy Facades layer.
            Document pdfDocument = new Document(inputPdf);

            // Ensure the document actually contains a form.
            if (pdfDocument.Form != null && pdfDocument.Form.Fields != null)
            {
                // Set value for the "FirstName" text field.
                if (pdfDocument.Form["FirstName"] is TextBoxField firstNameField)
                {
                    firstNameField.Value = "John";
                }

                // Set value for the "LastName" text field.
                if (pdfDocument.Form["LastName"] is TextBoxField lastNameField)
                {
                    lastNameField.Value = "Doe";
                }

                // Example for a checkbox/radio button field named "Subscribe".
                // Uncomment and adjust the type if such a field exists.
                // if (pdfDocument.Form["Subscribe"] is CheckBoxField subscribeField)
                // {
                //     subscribeField.Checked = true; // or false
                // }
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
            }

            // Save the modified PDF.
            pdfDocument.Save(outputPdf);

            Console.WriteLine($"Form fields updated and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
