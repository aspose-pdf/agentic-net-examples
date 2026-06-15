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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Rectangle where the date field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect)
            {
                Name = "DatePicker",
                DateFormat = "mm/dd/yyyy"
            };

            // Attach JavaScript that opens the calendar widget when the field is clicked
            // Use a valid action property from AnnotationActionCollection (OnPressMouseBtn)
            dateField.Actions.OnPressMouseBtn = new JavascriptAction("app.execMenuItem('ShowDatePicker');");

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date picker field added and saved to '{outputPath}'.");
    }
}
