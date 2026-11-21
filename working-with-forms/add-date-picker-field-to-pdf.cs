using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // Form field types
using Aspose.Pdf.Annotations;    // JavascriptAction and other annotation actions

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_date_picker.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle for the date picker field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box form field that will hold the selected date
            TextBoxField dateField = new TextBoxField(page, rect)
            {
                PartialName = "dateField",
                Value       = string.Empty
            };

            // Attach JavaScript that opens the built‑in date picker dialog when the field receives focus
            // Use a supported action property (OnEnter) instead of the non‑existent OnFocus
            dateField.Actions.OnEnter = new JavascriptAction("app.execMenuItem('ShowDatePicker');");

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date picker field added. Saved to '{outputPath}'.");
    }
}
