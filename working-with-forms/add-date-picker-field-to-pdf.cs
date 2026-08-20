using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect)
            {
                // Set a default date format (e.g., MM/dd/yyyy)
                DateFormat = "MM/dd/yyyy",
                // Set a tooltip (alternate name) for the field
                AlternateName = "Select a date",
                // Background color of the field
                Color = Color.LightGray
            };

            // Configure the border after the DateField instance has been created
            dateField.Border = new Border(dateField) { Width = 1 };

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Initialize the field (required for JavaScript actions to work)
            dateField.Init(doc.Pages[1]);

            // Attach JavaScript that opens the built‑in date picker when the field is activated
            dateField.OnActivated = new JavascriptAction("app.execMenuItem('ShowDatePicker');");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date picker field added. Saved to '{outputPath}'.");
    }
}
