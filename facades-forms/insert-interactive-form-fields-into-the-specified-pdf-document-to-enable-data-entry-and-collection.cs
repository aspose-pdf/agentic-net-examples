using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF and add interactive form fields
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPdf);

            // Add a text field for user name
            // Parameters: field type, field name, page number (1‑based), llx, lly, urx, ury
            formEditor.AddField(FieldType.Text, "NameField", 1, 100, 700, 300, 720);

            // Add a checkbox for agreement
            formEditor.AddField(FieldType.CheckBox, "AgreeCheck", 1, 100, 650, 120, 670);

            // Add a radio button group for gender (two options)
            // Both radios share the same field name; they will be mutually exclusive
            formEditor.AddField(FieldType.Radio, "Gender", 1, 100, 600, 120, 620); // Male
            formEditor.AddField(FieldType.Radio, "Gender", 1, 150, 600, 170, 620); // Female

            // Add a list box for country selection
            // Set the items before creating the field
            formEditor.Items = new string[] { "USA", "Canada", "UK", "Australia" };
            formEditor.AddField(FieldType.ListBox, "CountryList", 1, 100, 500, 200, 600);

            // Save the PDF with the newly added form fields
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Interactive form fields added and saved to '{outputPdf}'.");
    }
}