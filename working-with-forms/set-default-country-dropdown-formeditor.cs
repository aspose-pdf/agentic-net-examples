using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Existing PDF (can be empty)
        const string outputPdf = "output.pdf";  // PDF with the dropdown field

        // Define the position of the combo box on page 1 (coordinates in points)
        const int    pageNumber = 1;
        const float  llx = 100f;   // lower‑left X
        const float  lly = 700f;   // lower‑left Y
        const float  urx = 250f;   // upper‑right X
        const float  ury = 720f;   // upper‑right Y

        // Ensure the source file exists (create a blank PDF if it does not)
        if (!File.Exists(inputPdf))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Load the PDF document (core API)
        using (var doc = new Document(inputPdf))
        {
            // Use the FormEditor facade without importing its namespace.
            // Fully‑qualified name avoids the prohibited using directive.
            var formEditor = new Aspose.Pdf.Facades.FormEditor(doc);

            // Add a ComboBox (dropdown) field named "Country" with the default selected value "United States".
            // The overload with the initValue parameter sets the initial selection.
            bool added = formEditor.AddField(
                Aspose.Pdf.Facades.FieldType.ComboBox, // field type
                "Country",                             // field name
                "United States",                       // initial (default) value
                pageNumber,
                llx, lly, urx, ury);

            if (!added)
            {
                Console.Error.WriteLine("Failed to add the Country combo box field.");
                return;
            }

            // Optionally, define the list of items that appear in the dropdown.
            // This can be a simple string array; the first item matching the initValue will be selected.
            formEditor.Items = new string[]
            {
                "United States",
                "Canada",
                "United Kingdom",
                "Australia",
                "Germany"
            };

            // Save the modified document.
            formEditor.Save(outputPdf);
            formEditor.Close(); // Close the facade (not required but good practice)
        }

        Console.WriteLine($"PDF with Country dropdown saved to '{outputPdf}'.");
    }
}