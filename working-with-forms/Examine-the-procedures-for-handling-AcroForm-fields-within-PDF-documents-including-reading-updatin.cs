using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class AcroFormHandler
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Access the AcroForm object
                Form form = doc.Form;

                // -----------------------------------------------------------------
                // 1. READ: Enumerate all form fields and display their names/values
                // -----------------------------------------------------------------
                Console.WriteLine("Existing form fields:");
                foreach (Field field in form)
                {
                    // FullName is the qualified field name; Value holds the current content
                    Console.WriteLine($"- {field.FullName} = {field.Value}");
                }

                // ---------------------------------------------------------------
                // 2. UPDATE: Modify specific fields by name (if they exist)
                // ---------------------------------------------------------------
                // Example: set a text field named "CustomerName"
                const string targetFieldName = "CustomerName";
                if (form.HasField(targetFieldName))
                {
                    // The indexer returns a WidgetAnnotation; cast it to Field safely.
                    Field? targetField = form[targetFieldName] as Field;
                    if (targetField != null)
                    {
                        // Update the field's value
                        targetField.Value = "John Doe";

                        // Optionally set the field as read‑only after filling
                        targetField.ReadOnly = true;

                        Console.WriteLine($"Field '{targetFieldName}' updated to '{targetField.Value}'.");
                    }
                    else
                    {
                        // This should not happen if HasField returned true, but guard against unexpected types.
                        Console.WriteLine($"Field '{targetFieldName}' exists but is not a form field type that can be edited.");
                    }
                }
                else
                {
                    Console.WriteLine($"Field '{targetFieldName}' not found in the form.");
                }

                // ---------------------------------------------------------------
                // 3. VALIDATION: Perform a document integrity check
                // ---------------------------------------------------------------
                // The Check method validates the PDF structure. It throws an exception
                // if validation fails, so we wrap it in a try‑catch block.
                try
                {
                    // Pass 'true' to perform a deep validation (including form fields)
                    doc.Check(true);
                    Console.WriteLine("Document validation succeeded.");
                }
                catch (Exception validationEx)
                {
                    Console.Error.WriteLine($"Document validation failed: {validationEx.Message}");
                }

                // ---------------------------------------------------------------
                // 4. SAVE: Persist the modified PDF
                // ---------------------------------------------------------------
                // Use the standard Save method (no SaveOptions needed for PDF output)
                doc.Save(outputPdf);
                Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
