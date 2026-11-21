using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Correct namespace for form field types

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF into a Document object.
        // The Document is wrapped in a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Access the AcroForm (interactive form) collection.
            var acroForm = pdfDoc.Form;

            if (acroForm == null || acroForm.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
            }
            else
            {
                // Iterate over each form field and output basic information.
                foreach (Field field in acroForm)
                {
                    // FullName provides the field's hierarchical name.
                    // GetType().Name indicates the specific field type (e.g., TextBoxField, CheckboxField).
                    Console.WriteLine($"Field Name: {field.FullName}, Type: {field.GetType().Name}");
                }
            }
        }
    }
}
