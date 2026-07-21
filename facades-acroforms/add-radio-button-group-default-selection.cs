using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "radio_form.pdf";

        // Create a new PDF document and add a blank page.
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialise FormEditor on the newly created document.
            FormEditor formEditor = new FormEditor(doc);

            // Configure radio button appearance.
            formEditor.RadioGap = 5;                     // Gap between buttons (pixels).
            formEditor.RadioHoriz = true;                // Arrange horizontally.
            formEditor.Items = new string[] { "First", "Second", "Third" };

            // Add a radio button group with three options.
            // The third parameter ("Second") sets the default selected option.
            // Coordinates are given in points (1/72 inch).
            formEditor.AddField(
                FieldType.Radio,          // Field type.
                "SampleRadioGroup",       // Field name.
                "Second",                 // Default selected value.
                1,                        // Page number (1‑based).
                100, 500,                 // Lower‑left X,Y.
                200, 520);                // Upper‑right X,Y.

            // Save the modified PDF to disk.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}