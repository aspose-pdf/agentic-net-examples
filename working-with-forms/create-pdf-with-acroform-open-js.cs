using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithOpenJS.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define rectangle for the form field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field (widget annotation) and set its basic properties
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleTextField",
                Value = "Enter text here",
                Color = Aspose.Pdf.Color.LightGray
            };

            // Border must be assigned after the field instance exists (cannot be set inside the initializer)
            textField.Border = new Border(textField) { Width = 1 };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Document‑level JavaScript that runs when the PDF is opened
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
