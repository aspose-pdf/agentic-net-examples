using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document with at least two pages so that page 2 exists.
        using (Document doc = new Document())
        {
            // Add two blank pages.
            doc.Pages.Add();
            doc.Pages.Add();

            // Initialize the FormEditor facade with the in‑memory document.
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add an unchecked checkbox field named "AgreeTerms" on page 2.
                // Rectangle coordinates: lower‑left (100, 500), upper‑right (120, 520).
                formEditor.AddField(FieldType.CheckBox, "AgreeTerms", 2, 100f, 500f, 120f, 520f);

                // Save the modified PDF to disk.
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Checkbox field added and saved to '{outputPath}'.");
    }
}
