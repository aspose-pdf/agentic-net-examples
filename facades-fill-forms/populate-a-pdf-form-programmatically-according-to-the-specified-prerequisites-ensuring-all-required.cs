using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "template_form.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use the Form facade – it implements IDisposable, so wrap in using.
        using (Form form = new Form(inputPdf))
        {
            // Iterate over all field names present in the form.
            foreach (string fieldName in form.FieldNames)
            {
                // Simple strategy: try to fill every field with a string value.
                // For check boxes, radio buttons, etc., FillField(string, string) will be ignored
                // if the value is not appropriate, but it will not throw an exception.
                // You can adapt the value based on field type if needed.
                try
                {
                    form.FillField(fieldName, "SampleValue");
                }
                catch (Exception ex)
                {
                    // Log and continue – filling a field may fail if the type does not accept a string.
                    Console.Error.WriteLine($"Failed to fill field '{fieldName}': {ex.Message}");
                }
            }

            // Save the updated PDF to the desired output file.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdf}'.");
    }
}