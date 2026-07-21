using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "gender_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the form fields
            Page page = doc.Pages.Add();

            // Define the overall rectangle for the radio button field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create the radio button field on the page
            RadioButtonField genderField = new RadioButtonField(page);
            genderField.Name = "Gender";
            genderField.PartialName = "Gender";
            genderField.AlternateName = "Select Gender";
            genderField.NoToggleToOff = true;          // only one option can be selected
            genderField.Rect = fieldRect;              // set the field's bounding box

            // Define rectangles for individual options (positioned within the field rectangle)
            Aspose.Pdf.Rectangle maleOptionRect   = new Aspose.Pdf.Rectangle(100, 600, 115, 615);
            Aspose.Pdf.Rectangle femaleOptionRect = new Aspose.Pdf.Rectangle(130, 600, 145, 615);

            // Add the two gender options
            genderField.AddOption("Male",   maleOptionRect);
            genderField.AddOption("Female", femaleOptionRect);

            // Register the field with the document's AcroForm
            doc.Form.Add(genderField);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gender radio button saved to '{outputPath}'.");
    }
}