using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for AcroForm manipulation

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with AcroForm
        const string outputPdf = "unraveled_form.pdf"; // result PDF with fields flattened/removed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF as a Form facade
        Form form = new Form(inputPdf);

        // List all field names (optional, for diagnostics)
        Console.WriteLine("AcroForm fields found:");
        foreach (string name in form.FieldNames)
        {
            Console.WriteLine($" - {name}");
        }

        // Flatten all fields: values become part of the page content, fields are removed
        form.FlattenAllFields();

        // Example: remove a specific field (if it exists) after flattening
        const string fieldToRemove = "Signature";
        try
        {
            // FormEditor provides field‑removal capabilities
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPdf);               // bind the original PDF
                editor.RemoveField(fieldToRemove);      // delete the field
                editor.Save(outputPdf);                 // save the modified PDF
            }
        }
        catch (Exception ex)
        {
            // If the field does not exist or removal fails, fall back to the flattened PDF
            Console.WriteLine($"Field removal skipped: {ex.Message}");
            form.Save(outputPdf); // save the flattened version
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}