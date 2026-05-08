using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageForm.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // ---------- Page 1 ----------
            Page page1 = doc.Pages.Add();

            // Text box field on page 1
            TextBoxField txtField1 = new TextBoxField(page1,
                new Aspose.Pdf.Rectangle(100, 600, 300, 630));
            txtField1.PartialName = "Page1_TextBox";
            txtField1.Value = "Sample text";
            doc.Form.Add(txtField1);

            // Check box field on page 1
            CheckboxField chkField1 = new CheckboxField(page1,
                new Aspose.Pdf.Rectangle(100, 550, 120, 570));
            chkField1.PartialName = "Page1_CheckBox";
            chkField1.Checked = true; // default checked
            doc.Form.Add(chkField1);

            // ---------- Page 2 ----------
            Page page2 = doc.Pages.Add();

            // Radio button group on page 2
            RadioButtonField radioField = new RadioButtonField(page2);
            radioField.PartialName = "Page2_RadioGroup";
            radioField.Rect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);
            // Add two options to the radio group
            radioField.AddOption("OptionA");
            radioField.AddOption("OptionB");
            // Select the first option by default
            radioField.Selected = 1;
            doc.Form.Add(radioField);

            // List box field on page 2
            ListBoxField listBox = new ListBoxField(page2,
                new Aspose.Pdf.Rectangle(100, 500, 200, 560));
            listBox.PartialName = "Page2_ListBox";
            listBox.AddOption("Item1");
            listBox.AddOption("Item2");
            listBox.AddOption("Item3");
            // Select the second item by default
            listBox.Selected = 2;
            doc.Form.Add(listBox);

            // ---------- Page 3 ----------
            Page page3 = doc.Pages.Add();

            // Another text box on page 3
            TextBoxField txtField3 = new TextBoxField(page3,
                new Aspose.Pdf.Rectangle(100, 600, 300, 630));
            txtField3.PartialName = "Page3_TextBox";
            txtField3.Value = "Another page";
            doc.Form.Add(txtField3);

            // Another check box on page 3
            CheckboxField chkField3 = new CheckboxField(page3,
                new Aspose.Pdf.Rectangle(100, 550, 120, 570));
            chkField3.PartialName = "Page3_CheckBox";
            chkField3.Checked = false;
            doc.Form.Add(chkField3);

            // Save the PDF (inside the using block to keep the document alive)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑page PDF with distinct form fields saved to '{outputPath}'.");
    }
}