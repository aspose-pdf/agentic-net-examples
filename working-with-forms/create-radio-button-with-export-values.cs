using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "radio_button_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the radio button group will be placed
            // (left, bottom, right, top)
            Aspose.Pdf.Rectangle radioRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a RadioButtonField on the page (constructor takes only the Page)
            RadioButtonField radioField = new RadioButtonField(page);
            radioField.PartialName = "SampleRadioGroup"; // field name
            radioField.Rect = radioRect; // set the field's rectangle

            // Add options with specific export values
            // AddOption(exportValue, displayName)
            radioField.AddOption("VAL001", "Option A");
            radioField.AddOption("VAL002", "Option B");
            radioField.AddOption("VAL003", "Option C");

            // Add the radio button field to the document's form collection
            doc.Form.Add(radioField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button saved to '{outputPath}'.");
    }
}
