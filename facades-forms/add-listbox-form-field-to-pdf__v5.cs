using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "application_form.pdf";
        const string outputPath = "departments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and edit its form fields using FormEditor
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Define the rectangle for the ListBox on page 2 (llx, lly, urx, ury)
            float llx = 100f;
            float lly = 500f;
            float urx = 200f;
            float ury = 550f;

            // Items to display in the ListBox – set before adding the field
            editor.Items = new[] { "HR", "Finance", "IT", "Marketing" };

            // Add a ListBox field named "Departments" on page 2
            editor.AddField(FieldType.ListBox, "Departments", 2, llx, lly, urx, ury);

            // If you prefer adding items after the field is created, use AddListItem:
            // editor.AddListItem("Departments", "HR");
            // editor.AddListItem("Departments", "Finance");
            // editor.AddListItem("Departments", "IT");
            // editor.AddListItem("Departments", "Marketing");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"ListBox field added and saved to '{outputPath}'.");
    }
}
