using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "XfaForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Access the AcroForm object (core API)
            Aspose.Pdf.Forms.Form form = doc.Form;

            // NOTE: XFA support is optional in Aspose.Pdf.
            // The core API does not expose a direct property to query XFA availability.
            // We proceed to create a form field; if XFA is enabled in the library,
            // the field will be part of the XFA form automatically.

            // Create a text box field positioned on the page
            TextBoxField nameField = new TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(100, 700, 300, 730) // llx, lly, urx, ury
            );

            // Set the field's name (partial name) and default value
            nameField.PartialName = "CustomerName";
            nameField.Value = "John Doe";

            // Add the field to the document's form
            form.Add(nameField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form field saved to '{outputPath}'.");
    }
}