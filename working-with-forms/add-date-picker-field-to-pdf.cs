using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (creates one if it doesn't exist)
            Form form = doc.Form;

            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);
            dateField.Name = "DateField1";               // field name
            dateField.PartialName = "DateField1";        // partial name (used in JavaScript)
            dateField.AlternateName = "Select a date";   // tooltip shown in Acrobat
            dateField.DateFormat = "mm/dd/yyyy";        // display format

            // JavaScript that runs when the field is activated.
            // It sets the field value to the current date using the specified format.
            dateField.OnActivated = new JavascriptAction(
                "event.target.value = util.printd('mm/dd/yyyy', new Date());");

            // Add the field to the document's form
            form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date picker field added. Saved to '{outputPath}'.");
    }
}