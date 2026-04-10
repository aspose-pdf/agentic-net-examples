using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "date_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Instantiate a DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);
            dateField.Name = "DateOfBirth";                     // field identifier
            dateField.AlternateName = "Select your birth date"; // tooltip shown in Acrobat
            dateField.DateFormat = "MM/dd/yyyy";                // optional display format
            dateField.Required = true;                          // mark as required

            // Add the field to the document's interactive form
            doc.Form.Add(dateField);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}