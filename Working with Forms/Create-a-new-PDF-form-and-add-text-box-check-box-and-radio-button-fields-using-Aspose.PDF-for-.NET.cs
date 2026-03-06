using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class CreatePdfForm
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "CreatedForm.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // Adds the first page (page number = 1)

            // Initialize FormEditor with the document
            FormEditor formEditor = new FormEditor(doc);

            // -------------------------------------------------
            // Add a Text Box field
            // Parameters: FieldType, field name, page number, llx, lly, urx, ury
            // Coordinates are in points; origin is bottom‑left of the page
            // -------------------------------------------------
            formEditor.AddField(FieldType.Text, "TextBox1", 1, 50, 700, 250, 730);

            // -------------------------------------------------
            // Add a Check Box field
            // -------------------------------------------------
            formEditor.AddField(FieldType.CheckBox, "CheckBox1", 1, 300, 700, 320, 720);

            // -------------------------------------------------
            // Add a Radio Button group with two options
            // Set the Items property before adding the radio field
            // -------------------------------------------------
            formEditor.Items = new string[] { "OptionA", "OptionB" };
            // Optionally configure layout (horizontal arrangement, gap, size)
            formEditor.RadioHoriz = true;               // horizontal layout
            formEditor.RadioGap = 20;                   // gap between options
            formEditor.RadioButtonItemSize = 20;        // size of each radio button

            formEditor.AddField(FieldType.Radio, "RadioGroup1", 1, 350, 700, 450, 720);

            // Save the PDF with the newly added form fields
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF form created and saved to '{outputPath}'.");
    }
}