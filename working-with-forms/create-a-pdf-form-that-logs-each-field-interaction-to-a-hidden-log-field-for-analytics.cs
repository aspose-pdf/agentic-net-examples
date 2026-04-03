using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_log.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to a FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Add a hidden text field that will serve as the log
            // Parameters: field type, name, page number, llx, lly, urx, ury
            formEditor.AddField(FieldType.Text, "Log", 1, 0, 0, 0, 0);
            // Hide the log field from the user interface
            formEditor.SetFieldAppearance("Log", AnnotationFlags.Hidden);

            // Retrieve all existing field names using the Form facade
            Form form = new Form(inputPdf);
            foreach (string fieldName in form.FieldNames)
            {
                // Skip the log field itself
                if (fieldName == "Log")
                    continue;

                // JavaScript that appends a line to the hidden log field whenever the field is interacted with
                string script = $"var log = this.getField('Log'); log.value += '{fieldName} interacted\\n';";

                // Attach the script to the field
                formEditor.AddFieldScript(fieldName, script);
            }

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with interaction logging saved to '{outputPdf}'.");
    }
}