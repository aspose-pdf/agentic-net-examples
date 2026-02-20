using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document using Aspose.Pdf.Document (cross‑platform)
            Document pdfDocument = new Document(inputPdfPath);

            // -----------------------------------------------------------------
            // Example operations on form fields
            // -----------------------------------------------------------------

            // 1. Fill a text field (full field name required)
            try
            {
                // The Form collection returns a generic Field object. For a text field we need to cast to TextBoxField.
                var textField = pdfDocument.Form["FullName"] as TextBoxField;
                if (textField != null)
                {
                    textField.Value = "John Doe";
                }
                else
                {
                    Console.WriteLine("Field 'FullName' not found or is not a text field.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fill field failed: {ex.Message}");
            }

            // 2. Delete an unwanted field
            try
            {
                // Use Form.Delete to avoid ambiguity with IDictionary.Remove extension method.
                if (pdfDocument.Form["ObsoleteField"] != null)
                {
                    pdfDocument.Form.Delete("ObsoleteField");
                }
                else
                {
                    Console.WriteLine("Field 'ObsoleteField' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove field failed: {ex.Message}");
            }

            // 3. Rename an existing field
            try
            {
                // The PartialName property belongs to the base Field class.
                var field = pdfDocument.Form["OldFieldName"] as Field;
                if (field != null)
                {
                    field.PartialName = "NewFieldName";
                }
                else
                {
                    Console.WriteLine("Field 'OldFieldName' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Rename field failed: {ex.Message}");
            }

            // Save the modified PDF (save rule)
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Successfully edited form fields and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
