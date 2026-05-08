using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "radio_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Question 1 – Radio button group "Q1"
            // -------------------------------------------------
            // Create the radio button field on the page
            RadioButtonField q1Radio = new RadioButtonField(page);
            q1Radio.PartialName = "Q1";               // group name
            q1Radio.NoToggleToOff = false;            // allow no selection
            q1Radio.Color = Aspose.Pdf.Color.Black;  // border color

            // Add options (option name + rectangle where the button appears)
            q1Radio.AddOption("Option A", new Aspose.Pdf.Rectangle(100, 700, 110, 710));
            q1Radio.AddOption("Option B", new Aspose.Pdf.Rectangle(100, 680, 110, 690));
            q1Radio.AddOption("Option C", new Aspose.Pdf.Rectangle(100, 660, 110, 670));

            // Add the radio button field to the document's form collection
            doc.Form.Add(q1Radio);

            // -------------------------------------------------
            // Question 2 – Radio button group "Q2"
            // -------------------------------------------------
            RadioButtonField q2Radio = new RadioButtonField(page);
            q2Radio.PartialName = "Q2";
            q2Radio.NoToggleToOff = false;
            q2Radio.Color = Aspose.Pdf.Color.Black;

            q2Radio.AddOption("True",  new Aspose.Pdf.Rectangle(300, 700, 310, 710));
            q2Radio.AddOption("False", new Aspose.Pdf.Rectangle(300, 680, 310, 690));

            // Add the second radio button field to the document's form collection
            doc.Form.Add(q2Radio);

            // -------------------------------------------------
            // Save the document (no SaveOptions needed for PDF)
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button groups saved to '{outputPath}'.");
    }
}
