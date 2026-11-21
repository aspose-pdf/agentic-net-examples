using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePdf = "form.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Load the PDF document (which may contain form fields)
        Document pdf = new Document(sourcePdf);

        // Example: set a text field named "Name" if it exists
        var nameField = pdf.Form?.Fields?.FirstOrDefault(f => f.FullName == "Name") as TextBoxField;
        if (nameField != null)
        {
            nameField.Value = "John Doe";
        }

        // Example: check a checkbox named "Agree" if it exists
        var checkBox = pdf.Form?.Fields?.FirstOrDefault(f => f.FullName == "Agree");
        if (checkBox != null)
        {
            var prop = checkBox.GetType().GetProperty("Checked");
            if (prop != null && prop.CanWrite)
                prop.SetValue(checkBox, true);
            else
                checkBox.Value = "On"; // fallback for non‑standard checkboxes
        }

        // Save the filled PDF to a new file
        pdf.Save(outputPdf);

        Console.WriteLine($"Filled PDF saved to '{outputPdf}'.");
    }
}
