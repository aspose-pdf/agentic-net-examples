using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield_form.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100.0, 500.0, 300.0, 530.0);

            // Create the date picker field on the page
            DateField dateField = new DateField(page, rect);
            dateField.Name = "DateOfBirth";
            dateField.AlternateName = "Select your birth date";
            dateField.DateFormat = "MM/dd/yyyy";
            dateField.Required = true;

            // Add the field to the document's form collection and initialise it
            doc.Form.Add(dateField);
            dateField.Init(page);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with date picker field created: " + outputPath);
    }
}