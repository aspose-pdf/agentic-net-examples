using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "validated_form.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a required text box field
            TextBoxField nameField = new TextBoxField(page, fieldRect);
            nameField.PartialName = "NameField";
            nameField.Value = "";               // initially empty
            nameField.Required = true;          // mark as required (PDF flag)

            // Add the field to the document's AcroForm on page 1
            doc.Form.Add(nameField, 1);

            // Simulate form submission: if the required field is empty, highlight it
            if (string.IsNullOrWhiteSpace(nameField.Value))
            {
                // Border requires the parent annotation (the field itself) in the constructor
                nameField.Border = new Border(nameField);
                nameField.Border.Width = 2;                     // set border thickness
                nameField.Color = Aspose.Pdf.Color.Red;        // set border color to red
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validation saved to '{outputPath}'.");
    }
}