using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is at index 1)
            Page page = doc.Pages.Add();

            // Define the position and size of the checkbox (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            checkbox.Checked = true;

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF to a file
            doc.Save("checkbox_checked.pdf");
        }

        Console.WriteLine("PDF with checked checkbox saved as 'checkbox_checked.pdf'.");
    }
}