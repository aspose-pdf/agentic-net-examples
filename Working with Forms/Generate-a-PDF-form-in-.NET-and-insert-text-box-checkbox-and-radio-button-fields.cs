using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for Border class

class Program
{
    static void Main()
    {
        const string outputPath = "form_output.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ---------- Text Box ----------
            // Rectangle(left, bottom, width, height)
            var txtRect = new Rectangle(100, 600, 200, 30);
            TextBoxField textBox = new TextBoxField(page, txtRect)
            {
                Name = "txtName",
                PartialName = "txtName",
                Value = "Enter text here"
            };
            // Set border after the field is instantiated (Border requires the parent annotation)
            textBox.Border = new Border(textBox)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };
            doc.Form.Add(textBox);

            // ---------- Check Box ----------
            var chkRect = new Rectangle(100, 540, 20, 20);
            CheckboxField checkBox = new CheckboxField(page, chkRect)
            {
                Name = "chkAgree",
                PartialName = "chkAgree",
                ExportValue = "Yes", // value when checked
                Value = "Yes"        // set as checked by default
            };
            checkBox.Border = new Border(checkBox)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };
            doc.Form.Add(checkBox);

            // ---------- Radio Button Group ----------
            // Create the group container first
            RadioButtonField radioGroup = new RadioButtonField(page)
            {
                Name = "radGender",
                PartialName = "radGender"
            };

            // Option 1 – Male
            var opt1Rect = new Rectangle(100, 480, 20, 20);
            RadioButtonOptionField optionMale = new RadioButtonOptionField(page, opt1Rect)
            {
                Name = "radGenderMale",
                OptionName = "Male",
                Value = "Male" // select this option by default
            };
            // Add the option to the group BEFORE setting the border
            radioGroup.Add(optionMale);
            optionMale.Border = new Border(optionMale)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Option 2 – Female
            var opt2Rect = new Rectangle(100, 440, 20, 20);
            RadioButtonOptionField optionFemale = new RadioButtonOptionField(page, opt2Rect)
            {
                Name = "radGenderFemale",
                OptionName = "Female"
                // No default selection – leave Value null
            };
            radioGroup.Add(optionFemale);
            optionFemale.Border = new Border(optionFemale)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Finally add the whole radio group to the document form
            doc.Form.Add(radioGroup);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form created: {outputPath}");
    }
}
