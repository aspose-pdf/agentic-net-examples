using System;
using System.IO;
using Aspose.Pdf.Facades;   // Form, FormEditor classes
using Aspose.Pdf;          // Document class (optional for other operations)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with AcroForm
        const string outputPdf = "unraveled.pdf";      // result PDF after fields are removed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Load the form to obtain the list of field names.
            // -----------------------------------------------------------------
            Form formFacade = new Form(inputPdf);
            string[] fieldNames = formFacade.FieldNames;   // all AcroForm field names

            // -----------------------------------------------------------------
            // 2. Bind the same PDF to a FormEditor to perform deletions.
            // -----------------------------------------------------------------
            FormEditor editor = new FormEditor();
            editor.BindPdf(inputPdf);

            // -----------------------------------------------------------------
            // 3. Remove each field from the document.
            // -----------------------------------------------------------------
            foreach (string name in fieldNames)
            {
                // RemoveField silently ignores non‑existent names, so no extra checks needed.
                editor.RemoveField(name);
            }

            // -----------------------------------------------------------------
            // 4. Save the modified PDF.
            // -----------------------------------------------------------------
            editor.Save(outputPdf);

            // Optional: close the editor (releases internal resources).
            editor.Close();

            Console.WriteLine($"AcroForm fields removed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}