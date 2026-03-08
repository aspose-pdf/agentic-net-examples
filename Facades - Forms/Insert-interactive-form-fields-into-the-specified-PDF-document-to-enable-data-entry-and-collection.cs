using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // PDF that will receive form fields
        const string outputPath = "filled_form.pdf"; // Resulting PDF with interactive fields

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the FormEditor facade and bind the source PDF
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // -------------------------------------------------
        // Add various interactive fields (page numbers are 1‑based)
        // -------------------------------------------------

        // Text field for full name
        formEditor.AddField(FieldType.Text, "FullName", 1, 100f, 700f, 300f, 720f);

        // Check box for newsletter subscription
        formEditor.AddField(FieldType.CheckBox, "Subscribe", 1, 100f, 650f, 120f, 670f);

        // Radio button group for gender (two separate radio fields)
        formEditor.AddField(FieldType.Radio, "Gender_Male",   1, 100f, 600f, 120f, 620f);
        formEditor.AddField(FieldType.Radio, "Gender_Female", 1, 150f, 600f, 170f, 620f);

        // List box for country selection
        formEditor.Items = new string[] { "USA", "Canada", "UK", "Australia" };
        formEditor.AddField(FieldType.ListBox, "Country", 1, 100f, 540f, 250f, 580f);

        // Submit button that posts data to a URL
        formEditor.AddSubmitBtn(
            "Submit",               // button name
            1,                      // page number
            "https://example.com/submit", // target URL
            "Submit",               // button label
            100f, 500f, 200f, 530f // rectangle (llx, lly, urx, ury)
        );

        // Save the PDF with the newly added form fields
        formEditor.Save(outputPath);
        Console.WriteLine($"Interactive form fields added and saved to '{outputPath}'.");
    }
}