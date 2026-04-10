using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // Added for TextFragment and Position

class CreateRadioButtonForm
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "RadioButtonForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Question 1: "What is the capital of France?"
            // -----------------------------------------------------------------
            // Create a RadioButtonField for the question (group name: Q1)
            RadioButtonField q1Field = new RadioButtonField(page);
            q1Field.PartialName = "Q1";               // internal name of the group
            q1Field.AlternateName = "Capital of France"; // tooltip
            q1Field.NoToggleToOff = false;            // allow no selection

            // Define rectangles for each option (positioned vertically)
            // Note: Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle opt1Rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720); // "Paris"
            Aspose.Pdf.Rectangle opt2Rect = new Aspose.Pdf.Rectangle(100, 680, 120, 700); // "Berlin"
            Aspose.Pdf.Rectangle opt3Rect = new Aspose.Pdf.Rectangle(100, 660, 120, 680); // "Madrid"

            // Add options to the radio button group
            q1Field.AddOption("Paris", opt1Rect);
            q1Field.AddOption("Berlin", opt2Rect);
            q1Field.AddOption("Madrid", opt3Rect);

            // Add the field to the document's form collection
            doc.Form.Add(q1Field);

            // -----------------------------------------------------------------
            // Question 2: "Select the primary color."
            // -----------------------------------------------------------------
            RadioButtonField q2Field = new RadioButtonField(page);
            q2Field.PartialName = "Q2";
            q2Field.AlternateName = "Primary Color";
            q2Field.NoToggleToOff = false;

            // Rectangles for options
            Aspose.Pdf.Rectangle q2Opt1 = new Aspose.Pdf.Rectangle(300, 700, 320, 720); // "Red"
            Aspose.Pdf.Rectangle q2Opt2 = new Aspose.Pdf.Rectangle(300, 680, 320, 700); // "Green"
            Aspose.Pdf.Rectangle q2Opt3 = new Aspose.Pdf.Rectangle(300, 660, 320, 680); // "Blue"

            // Add options
            q2Field.AddOption("Red", q2Opt1);
            q2Field.AddOption("Green", q2Opt2);
            q2Field.AddOption("Blue", q2Opt3);

            // Add the second field to the form
            doc.Form.Add(q2Field);

            // -----------------------------------------------------------------
            // Optional: Add visible labels for the questions and options
            // -----------------------------------------------------------------
            // Question 1 label
            TextFragment q1Label = new TextFragment("1. What is the capital of France?");
            q1Label.Position = new Position(100, 730);
            page.Paragraphs.Add(q1Label);

            // Option labels for Question 1
            page.Paragraphs.Add(new TextFragment("Paris")   { Position = new Position(130, 710) });
            page.Paragraphs.Add(new TextFragment("Berlin")  { Position = new Position(130, 690) });
            page.Paragraphs.Add(new TextFragment("Madrid")  { Position = new Position(130, 670) });

            // Question 2 label
            TextFragment q2Label = new TextFragment("2. Select the primary color.");
            q2Label.Position = new Position(300, 730);
            page.Paragraphs.Add(q2Label);

            // Option labels for Question 2
            page.Paragraphs.Add(new TextFragment("Red")   { Position = new Position(340, 710) });
            page.Paragraphs.Add(new TextFragment("Green") { Position = new Position(340, 690) });
            page.Paragraphs.Add(new TextFragment("Blue")  { Position = new Position(340, 670) });

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button groups saved to '{outputPath}'.");
    }
}
