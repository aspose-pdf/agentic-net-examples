using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add three pages, each with its own set of form fields
            for (int pageNumber = 1; pageNumber <= 3; pageNumber++)
            {
                // Add a blank page (1‑based indexing)
                Page page = doc.Pages.Add();

                // Define a rectangle for the primary field on this page
                double llx = 100;
                double lly = 600 - (pageNumber - 1) * 150;
                double urx = 300;
                double ury = 650 - (pageNumber - 1) * 150;
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a distinct field type per page
                switch (pageNumber)
                {
                    case 1:
                        // Text box on page 1
                        TextBoxField txtField = new TextBoxField(page, rect)
                        {
                            PartialName = "TextBox_Page1",
                            Value = "Enter text here"
                        };
                        doc.Form.Add(txtField, pageNumber);
                        break;

                    case 2:
                        // Check box on page 2
                        CheckboxField chkField = new CheckboxField(page, rect)
                        {
                            PartialName = "CheckBox_Page2",
                            // "Off" is the default state; no need to set Value explicitly
                        };
                        doc.Form.Add(chkField, pageNumber);
                        break;

                    case 3:
                        // Radio button group on page 3 (two options)
                        // First option
                        RadioButtonField radio1 = new RadioButtonField(page)
                        {
                            Rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520),
                            PartialName = "RadioGroup_Page3_Option1",
                            // Do not set Value before the field is added to the form – it causes NullReferenceException
                            // Use Checked to indicate selection state (default is false)
                        };

                        // Second option
                        RadioButtonField radio2 = new RadioButtonField(page)
                        {
                            Rect = new Aspose.Pdf.Rectangle(150, 500, 170, 520),
                            PartialName = "RadioGroup_Page3_Option2",
                        };

                        // Add both radio buttons to the form on this page
                        doc.Form.Add(radio1, pageNumber);
                        doc.Form.Add(radio2, pageNumber);
                        break;
                }
            }

            // Save the PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with distinct AcroForm fields saved to '{outputPath}'.");
    }
}
