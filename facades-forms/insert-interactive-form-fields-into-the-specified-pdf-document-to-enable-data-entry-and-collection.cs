using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "form_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor and bind it to the loaded document
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(doc);

            // Add a single‑line text field for the customer's name
            // Parameters: field type, field name, page number (1‑based), llx, lly, urx, ury
            formEditor.AddField(FieldType.Text, "CustomerName", 1, 100f, 700f, 300f, 720f);

            // Add a checkbox field for newsletter subscription
            formEditor.AddField(FieldType.CheckBox, "SubscribeNewsletter", 1, 100f, 650f, 120f, 670f);

            // Add two radio button fields for gender selection
            formEditor.AddField(FieldType.Radio, "Gender_Male", 1, 100f, 600f, 120f, 620f);
            formEditor.AddField(FieldType.Radio, "Gender_Female", 1, 150f, 600f, 170f, 620f);

            // Add a list box field for country selection
            formEditor.AddField(FieldType.ListBox, "CountryList", 1, 100f, 500f, 250f, 580f);
            // Populate the list box with items
            formEditor.Items = new string[] { "USA", "Canada", "UK", "Australia" };

            // Add a submit button that posts to a URL
            // Parameters: button name, page number, URL, button label, llx, lly, urx, ury
            formEditor.AddSubmitBtn("SubmitForm", 1, "http://example.com/submit", "Submit", 100f, 450f, 200f, 470f);

            // Save the modified PDF with the new interactive fields
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Interactive form saved to '{outputPath}'.");
    }
}