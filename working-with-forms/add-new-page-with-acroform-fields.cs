using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page (1‑based indexing, the new page becomes the last page)
            Page newPage = doc.Pages.Add();

            // ---------- Text Box Field ----------
            // Define the field rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            // Create the text box field on the new page
            TextBoxField txtField = new TextBoxField(newPage, txtRect);
            txtField.PartialName = "NewTextField";
            txtField.Contents = "Enter text here";
            // Add the field to the form, specifying the page number (last page)
            doc.Form.Add(txtField, doc.Pages.Count);

            // ---------- Check Box Field ----------
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);
            // Correct class name is CheckboxField (lowercase 'b')
            CheckboxField chkField = new CheckboxField(newPage, chkRect);
            chkField.PartialName = "NewCheckBox";
            chkField.Contents = "Check me";
            doc.Form.Add(chkField, doc.Pages.Count);

            // ---------- Radio Button Group ----------
            // Create the radio button container (no rectangle needed for the group itself)
            RadioButtonField radioGroup = new RadioButtonField(newPage);
            radioGroup.PartialName = "NewRadioGroup";

            // First option
            Aspose.Pdf.Rectangle opt1Rect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);
            RadioButtonOptionField opt1 = new RadioButtonOptionField(newPage, opt1Rect);
            opt1.OptionName = "Option1";
            opt1.Caption = new TextFragment("Option 1");
            radioGroup.Add(opt1);

            // Second option (positioned lower)
            Aspose.Pdf.Rectangle opt2Rect = new Aspose.Pdf.Rectangle(100, 560, 120, 580);
            RadioButtonOptionField opt2 = new RadioButtonOptionField(newPage, opt2Rect);
            opt2.OptionName = "Option2";
            opt2.Caption = new TextFragment("Option 2");
            radioGroup.Add(opt2);

            // Add the radio group to the form
            doc.Form.Add(radioGroup, doc.Pages.Count);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
