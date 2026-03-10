using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // ---------- Fill existing AcroForm fields ----------
        // Load the source PDF into a Document object.
        Document srcDoc = new Document(inputPdf);
        using (Form form = new Form(srcDoc))
        {
            // Fill a text field (field name must be fully qualified).
            form.FillField("CustomerName", "John Doe");

            // Fill a checkbox field (true = checked, false = unchecked).
            form.FillField("AgreeTerms", true);

            // Fill a radio button group by index (0‑based index of the option).
            form.FillField("Gender", 1);

            // Save the modifications to a new file.
            form.Save(outputPdf);
        }

        // ---------- Add a new field and flatten the form ----------
        // Load the just‑saved PDF so we can edit it further.
        Document outDoc = new Document(outputPdf);
        using (FormEditor editor = new FormEditor(outDoc))
        {
            // Add a new text field on page 1.
            // Parameters: field type, field name, page number, llx, lly, urx, ury.
            editor.AddField(FieldType.Text, "NewComment", 1, 100, 500, 300, 520);
        }

        // After the field is added we can set its default value and flatten the form
        // using the Form facade (FormEditor does not expose FillField/FlattenAllFields).
        using (Form form = new Form(outDoc))
        {
            // Set a default value for the newly added field.
            form.FillField("NewComment", "Enter comment here");

            // Flatten all fields so their appearances become part of the page content.
            form.FlattenAllFields();

            // Save the final document (overwrites the same file).
            form.Save(outputPdf);
        }

        Console.WriteLine($"AcroForm manipulation completed. Output saved to '{outputPdf}'.");
    }
}
