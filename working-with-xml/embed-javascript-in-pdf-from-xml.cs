using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the XML file and convert it to PDF using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Embed a JavaScript that runs when the document is opened
            pdfDoc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Ensure there is at least one page to place the button on
            Page page = pdfDoc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a button field – the correct constructor takes (Page, Rectangle)
            ButtonField validateButton = new ButtonField(page, buttonRect)
            {
                PartialName = "ValidateButton",
                Value = "Validate"
            };

            // Attach JavaScript to the button's mouse‑release event using a valid action property
            validateButton.Actions.OnReleaseMouseBtn = new JavascriptAction(
                "app.alert('Form validation logic goes here');");

            // Add the button to the document's form collection
            pdfDoc.Form.Add(validateButton);

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{outputPdf}'.");
    }
}
