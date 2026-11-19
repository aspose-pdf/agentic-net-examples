using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Ensure the document has at least one page (Aspose.Pdf uses 1‑based indexing).
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page.
            Page page = doc.Pages[1];

            // Create a text box form field for user input.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField nameField = new TextBoxField(page, rect)
            {
                PartialName = "Name",
                Value = ""
            };

            // Set the border using the Border class (requires the field as parent).
            nameField.Color = Aspose.Pdf.Color.Black; // border color
            nameField.Border = new Border(nameField) { Width = 1 };

            // Add JavaScript validation that runs when the field loses focus.
            nameField.Actions.OnValidate = new JavascriptAction(
                "if (event.value == '') { app.alert('Name is required'); event.rc = false; } else { event.rc = true; }"
            );

            // Add the field to the document's form collection.
            doc.Form.Add(nameField);

            // Optional: JavaScript that runs when the PDF is opened.
            doc.OpenAction = new JavascriptAction("app.alert('PDF loaded');");

            // Save the resulting PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{outputPdf}'.");
    }
}
