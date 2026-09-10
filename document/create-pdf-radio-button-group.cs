using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // Added for TextFragment and Position

class Program
{
    static void Main()
    {
        const string outputPath = "multiple_choice_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a radio button field (group) attached to the page
            RadioButtonField radioGroup = new RadioButtonField(page)
            {
                // Field name used to identify the group (canonical property is PartialName)
                PartialName = "Question1",
                // Optional: allow no selection (set to false to let user deselect)
                NoToggleToOff = false
            };

            // Define option rectangles (position and size) on the page
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle opt1Rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            Aspose.Pdf.Rectangle opt2Rect = new Aspose.Pdf.Rectangle(100, 660, 120, 680);
            Aspose.Pdf.Rectangle opt3Rect = new Aspose.Pdf.Rectangle(100, 620, 120, 640);

            // Add options to the radio button group
            radioGroup.AddOption("Option A", opt1Rect);
            radioGroup.AddOption("Option B", opt2Rect);
            radioGroup.AddOption("Option C", opt3Rect);

            // Add the radio button field to the document's form
            doc.Form.Add(radioGroup);

            // Optionally, add visible labels for the options using TextFragment
            page.Paragraphs.Add(new TextFragment("Question 1: Choose an answer"));
            page.Paragraphs.Add(new TextFragment("A") { Position = new Position(80, 710) });
            page.Paragraphs.Add(new TextFragment("B") { Position = new Position(80, 670) });
            page.Paragraphs.Add(new TextFragment("C") { Position = new Position(80, 630) });

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}
