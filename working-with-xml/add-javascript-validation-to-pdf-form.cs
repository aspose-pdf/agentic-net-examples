using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";          // XML source
        const string outputPdf    = "output.pdf";         // Resulting PDF

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create a new PDF document and bind the XML data.
        using (Document doc = new Document())
        {
            // BindXml parses the XML and creates the PDF structure.
            doc.BindXml(xmlPath);

            // -----------------------------------------------------------------
            // Example: add a TextBox form field and attach JavaScript validation.
            // -----------------------------------------------------------------
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1];

            // Define the field rectangle (left, bottom, right, top).
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the TextBox field on the page.
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "UserName",          // field name
                Value       = "",                  // initial value
                Color       = Aspose.Pdf.Color.LightGray
            };

            // Set a visible border (requires the parent annotation in the constructor).
            txtField.Border = new Border(txtField) { Width = 1 };

            // Attach JavaScript that validates the field when the user leaves it.
            // The script checks that the entered value has at least 5 characters.
            txtField.Actions.OnValidate = new JavascriptAction(
                "if (event.value.length < 5) { " +
                "app.alert('Please enter at least 5 characters.'); " +
                "event.rc = false; " + // reject the change
                "}"
            );

            // Add the field to the document's form on page 1.
            doc.Form.Add(txtField, 1);

            // -----------------------------------------------------------------
            // Optional: add a document‑level JavaScript action (executed on open).
            // -----------------------------------------------------------------
            // The Document class exposes an OpenAction property that can hold a JavascriptAction.
            doc.OpenAction = new JavascriptAction("app.alert('Welcome to the generated PDF!');");

            // Save the final PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with JavaScript actions: {outputPdf}");
    }
}