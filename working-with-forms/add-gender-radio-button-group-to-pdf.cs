using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "GenderForm.pdf";

        // Create a new PDF document and add a page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a radio button field bound to the page.
            RadioButtonField genderField = new RadioButtonField(page)
            {
                // Logical name of the field.
                PartialName = "Gender",
                // Tooltip shown in PDF viewers.
                AlternateName = "Select Gender",
                // Allow no option to be selected initially.
                NoToggleToOff = false
            };

            // Define rectangles for each option (coordinates are in points).
            // Male option.
            Aspose.Pdf.Rectangle maleRect = new Aspose.Pdf.Rectangle(100, 700, 110, 710);
            // Female option.
            Aspose.Pdf.Rectangle femaleRect = new Aspose.Pdf.Rectangle(100, 680, 110, 690);

            // Add the options to the radio button field.
            genderField.AddOption("Male", maleRect);
            genderField.AddOption("Female", femaleRect);

            // Add the radio button field to the document's AcroForm.
            doc.Form.Add(genderField);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gender radio button group saved to '{outputPath}'.");
    }
}