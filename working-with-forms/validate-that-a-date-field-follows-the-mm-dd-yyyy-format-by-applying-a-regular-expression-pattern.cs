using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the date field
            Page page = doc.Pages[1];

            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // Create a DateField instance and add it to the form
            DateField dateField = new DateField(page, rect);
            // Set the visual date format to MM/dd/yyyy
            dateField.DateFormat = "MM/dd/yyyy";
            doc.Form.Add(dateField);

            // JavaScript that validates the entered value against MM/DD/YYYY pattern
            string js = @"
var re = /^\d{2}\/\d{2}\/\d{4}$/;
if (!re.test(this.value)) {
    app.alert('Please enter date in MM/DD/YYYY format.');
    this.focus();
}";
            // Attach the script to the field's OnValidate action using the correct class name
            dateField.Actions.OnValidate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with date field validation to '{outputPath}'.");
    }
}
