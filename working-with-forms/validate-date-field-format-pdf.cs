using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;   // for JavascriptAction

class Program
{
    static void Main()
    {
        // Path to the output PDF
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the date field
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the date field and add it to the document's form collection
            DateField dateField = new DateField(page, rect);
            doc.Form.Add(dateField);

            // Set the visual date format (optional, shows the expected format)
            dateField.DateFormat = "MM/dd/yyyy";

            // Add a JavaScript action that validates the entered value against MM/DD/YYYY
            // The action runs when the field loses focus (OnLostFocus). Adjust as needed.
            string js = @"
                var re = /^\d{2}\/\d{2}\/\d{4}$/;
                if (!re.test(this.value)) {
                    app.alert('Invalid date format. Please use MM/DD/YYYY.');
                    // Optionally clear the invalid value
                    this.value = '';
                }
            ";
            // Use the correct action property for losing focus
            dateField.Actions.OnLostFocus = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated date field saved to '{outputPath}'.");
    }
}
