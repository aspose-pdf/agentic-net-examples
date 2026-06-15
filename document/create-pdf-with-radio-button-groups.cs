using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "quiz.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page where the form fields will be placed
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Question 1 – multiple‑choice radio button group
            // -------------------------------------------------
            // Create the radio button field and associate it with the page
            RadioButtonField q1 = new RadioButtonField(page);
            q1.PartialName = "Q1";               // internal field name
            q1.AlternateName = "Question 1";     // tooltip shown in Acrobat

            // Add three options (A, B, C) with their visual rectangles
            // Rectangle(left, bottom, right, top) – coordinates are in points
            q1.AddOption("OptionA", new Aspose.Pdf.Rectangle(100, 700, 110, 710));
            q1.AddOption("OptionB", new Aspose.Pdf.Rectangle(100, 680, 110, 690));
            q1.AddOption("OptionC", new Aspose.Pdf.Rectangle(100, 660, 110, 670));

            // Register the field with the document's form collection
            doc.Form.Add(q1);

            // -------------------------------------------------
            // Question 2 – another radio button group
            // -------------------------------------------------
            RadioButtonField q2 = new RadioButtonField(page);
            q2.PartialName = "Q2";
            q2.AlternateName = "Question 2";

            // Two options: Yes and No
            q2.AddOption("Yes", new Aspose.Pdf.Rectangle(300, 700, 310, 710));
            q2.AddOption("No",  new Aspose.Pdf.Rectangle(300, 680, 310, 690));

            doc.Form.Add(q2);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button groups saved to '{outputPath}'.");
    }
}