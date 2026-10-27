using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "DateTimeField.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box form field that will hold the date/time
            TextBoxField dateTimeField = new TextBoxField(page, fieldRect);
            dateTimeField.PartialName = "DateTimeField";
            dateTimeField.Value = "Loading..."; // placeholder, will be overwritten by JavaScript

            // Set visual appearance – border (background color is not supported on TextBoxField in older versions)
            dateTimeField.Border = new Border(dateTimeField) { Width = 1, Style = BorderStyle.Solid };
            // Optional: set border color if desired
            // dateTimeField.Color = Aspose.Pdf.Color.LightGray;

            // Add the field to the document's form collection
            doc.Form.Add(dateTimeField);

            // JavaScript that runs when the document is opened.
            // It sets the field's value to the current date and time.
            string js = "this.getField('DateTimeField').value = util.printd('mm/dd/yyyy HH:MM:ss', new Date());";

            // Attach the JavaScript to the document's OpenAction (the correct way to add document‑level JavaScript)
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic date/time field saved to '{outputPath}'.");
    }
}
