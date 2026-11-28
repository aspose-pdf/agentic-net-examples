using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "registration_form_v1.pdf";
        const string outputPath = "required_country.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create a FormEditor and bind it to the loaded document
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(doc);

            // Rectangle where the ListBox will be placed (llx, lly, urx, ury)
            float llx = 100f, lly = 500f, urx = 200f, ury = 550f;

            // Add a ListBox field named "Country" on page 1
            editor.AddField(FieldType.ListBox, "Country", 1, llx, lly, urx, ury);

            // Populate the ListBox items
            string[] countryItems = { "USA", "Canada", "UK", "Australia" };
            foreach (var item in countryItems)
                editor.AddListItem("Country", item);

            // Persist the changes made by the editor
            editor.Save();
        }

        // Mark the field as required
        if (doc.Form["Country"] != null)
        {
            doc.Form["Country"].Required = true;
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Form with required ListBox saved to '{outputPath}'.");
    }
}
