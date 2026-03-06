using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_flattened.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF as a form using the Facade API
        using (Form form = new Form(inputPdf))
        {
            // Fill each form field with a sample value.
            // Adjust the value or add type‑specific handling as required.
            foreach (string fieldName in form.FieldNames)
            {
                form.FillField(fieldName, "Sample Value");
            }

            // Convert all interactive fields into static page content.
            form.FlattenAllFields();

            // Persist the changes to a new file.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form fields set and flattened. Saved to '{outputPdf}'.");
    }
}