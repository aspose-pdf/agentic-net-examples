using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define page margins (points)
            page.PageInfo.Margin = new MarginInfo { Top = 50, Bottom = 50, Left = 50, Right = 50 };

            // Retrieve useful dimensions
            double marginLeft = page.PageInfo.Margin.Left;
            double marginTop = page.PageInfo.Margin.Top;
            double pageHeight = page.PageInfo.Height; // default A4 height

            // -------------------------------------------------
            // Add a TextBox field positioned relative to margins
            // -------------------------------------------------
            double txtWidth = 200;
            double txtHeight = 20;
            double txtX = marginLeft;
            double txtY = pageHeight - marginTop - txtHeight; // distance from top margin

            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(txtX, txtY, txtX + txtWidth, txtY + txtHeight);
            TextBoxField txtField = new TextBoxField(doc, txtRect)
            {
                PartialName = "CustomerName",
                Value = "John Doe"
            };
            // Place the field on page 1
            doc.Form.Add(txtField, 1);

            // -------------------------------------------------
            // Add a RadioButton group positioned below the TextBox
            // -------------------------------------------------
            double gap = 30; // space between controls
            double rbY = txtY - gap - 15; // 15 points height for each radio option
            double rbX = marginLeft;

            // Create the container radio button field
            RadioButtonField radioField = new RadioButtonField(doc)
            {
                PartialName = "Gender"
            };

            // Option 1: Male
            Aspose.Pdf.Rectangle opt1Rect = new Aspose.Pdf.Rectangle(rbX, rbY, rbX + 15, rbY + 15);
            RadioButtonOptionField opt1 = new RadioButtonOptionField(page, opt1Rect)
            {
                OptionName = "Male",
                Value = "Male"
            };

            // Option 2: Female (offset horizontally)
            Aspose.Pdf.Rectangle opt2Rect = new Aspose.Pdf.Rectangle(rbX + 80, rbY, rbX + 95, rbY + 15);
            RadioButtonOptionField opt2 = new RadioButtonOptionField(page, opt2Rect)
            {
                OptionName = "Female",
                Value = "Female"
            };

            // Attach options to the radio field
            radioField.Add(opt1);
            radioField.Add(opt2);

            // Place the radio field on page 1
            doc.Form.Add(radioField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}