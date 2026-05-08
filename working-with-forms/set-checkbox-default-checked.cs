using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

            // Create the checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            checkbox.Checked = true; // alternatively: checkbox.Value = "On";

            // Add the checkbox to the document's form
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with checked checkbox saved as 'output.pdf'.");
    }
}