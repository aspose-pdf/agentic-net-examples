using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_placeholder.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the textbox will appear
            var rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a textbox form field on the page
            TextBoxField textField = new TextBoxField(page, rect);
            textField.PartialName = "NameField";
            textField.Value = "Enter name here"; // placeholder text (initial value)

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Tag the field with a Form structure element for accessibility
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            FormElement formElem = tagged.CreateFormElement();
            formElem.AlternativeText = "Form field for name entry";
            // Associate the form field directly (form fields are annotations themselves)
            formElem.Tag(textField);
            root.AppendChild(formElem);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with form field saved to 'form_with_placeholder.pdf'.");
    }
}
