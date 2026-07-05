using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the form field (llx, lly, urx, ury)
            var fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box form field on the page
            var textBox = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",   // field identifier
                Value = "Enter text here"      // default value
            };

            // Set the border color (border color is controlled by the annotation's Color property)
            textBox.Color = Aspose.Pdf.Color.Black;

            // Configure the border to be beveled (3‑D appearance)
            // Border must be created with the parent annotation (the field itself) and set after the field is instantiated
            textBox.Border = new Border(textBox)
            {
                Style = BorderStyle.Beveled, // beveled style
                Width = 2                    // thickness in points
            };

            // Add the form field to the document's form collection (not directly to page annotations)
            doc.Form.Add(textBox);

            // Save the PDF to disk
            string outputPath = "FormFieldBeveledBorder.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
