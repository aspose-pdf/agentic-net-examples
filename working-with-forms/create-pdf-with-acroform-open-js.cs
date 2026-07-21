using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithOpenJS.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single blank page (required for placing fields)
            Page page = doc.Pages.Add();

            // Define the rectangle for the text field
            var rect = new Rectangle(100, 500, 300, 520);

            // Create a text field (AcroForm) and associate it with the page
            var textField = new TextBoxField(page, rect)
            {
                // Optional default value
                Value = "Enter text here"
            };
            // Set the field's name (PartialName)
            textField.PartialName = "SampleField";

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Set a document‑level JavaScript that runs when the PDF is opened
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with AcroForm field and open‑action JavaScript: {outputPath}");
    }
}