using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "radio_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Question 1
            // -------------------------------------------------
            // Add the question text
            TextFragment question1 = new TextFragment("1. What is your favorite color?");
            question1.Position = new Position(50, 750); // top-left corner
            page.Paragraphs.Add(question1);

            // Create a radio button group for the first question
            RadioButtonField radioGroup1 = new RadioButtonField(page);
            radioGroup1.Name = "Q1";
            radioGroup1.PartialName = "Q1";
            // Allow the user to leave the group unselected (optional)
            radioGroup1.NoToggleToOff = false;

            // Add options with their visual rectangles
            radioGroup1.AddOption("Red",   new Aspose.Pdf.Rectangle(50, 720, 70, 740));
            radioGroup1.AddOption("Green", new Aspose.Pdf.Rectangle(50, 690, 70, 710));
            radioGroup1.AddOption("Blue",  new Aspose.Pdf.Rectangle(50, 660, 70, 680));

            // Add the radio button field to the document's form
            doc.Form.Add(radioGroup1);

            // -------------------------------------------------
            // Question 2
            // -------------------------------------------------
            // Add the second question text
            TextFragment question2 = new TextFragment("2. Choose your preferred transport:");
            question2.Position = new Position(50, 620);
            page.Paragraphs.Add(question2);

            // Create a second radio button group
            RadioButtonField radioGroup2 = new RadioButtonField(page);
            radioGroup2.Name = "Q2";
            radioGroup2.PartialName = "Q2";
            radioGroup2.NoToggleToOff = false;

            // Add options for the second question
            radioGroup2.AddOption("Car",    new Aspose.Pdf.Rectangle(50, 590, 70, 610));
            radioGroup2.AddOption("Bicycle",new Aspose.Pdf.Rectangle(50, 560, 70, 580));
            radioGroup2.AddOption("Train",  new Aspose.Pdf.Rectangle(50, 530, 70, 550));
            radioGroup2.AddOption("Plane",  new Aspose.Pdf.Rectangle(50, 500, 70, 520));

            // Add the second radio button field to the form
            doc.Form.Add(radioGroup2);

            // Save the PDF document (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button groups saved to '{outputPath}'.");
    }
}