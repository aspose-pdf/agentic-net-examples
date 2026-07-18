using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the form field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box form field on the page
            TextBoxField textBox = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",   // internal name of the field
                Value = "Enter text here"      // default displayed text
            };

            // Set the border style to Beveled (three‑dimensional appearance)
            textBox.Border = new Border(textBox)
            {
                Style = BorderStyle.Beveled
            };

            // Add the form field to the document's AcroForm collection.
            // The second argument specifies the page number where the widget should be placed.
            doc.Form.Add(textBox, 1);

            // Save the PDF with the beveled form field border
            doc.Save("form_beveled.pdf");
        }

        Console.WriteLine("PDF with beveled form field border saved as 'form_beveled.pdf'.");
    }
}
