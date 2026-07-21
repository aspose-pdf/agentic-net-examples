using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "RadioButtonForm.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Add a question text
            TextFragment question = new TextFragment("1. What is the capital of France?");
            question.TextState.Font = FontRepository.FindFont("Helvetica");
            question.TextState.FontSize = 12;
            question.Position = new Position(50, 750);
            page.Paragraphs.Add(question);

            // Create a RadioButtonField (group) for the question using the page‑based constructor
            // Fully qualify Aspose.Pdf.Rectangle to avoid any ambiguity with System.Drawing.Rectangle
            RadioButtonField radioGroup = new RadioButtonField(page);
            radioGroup.Name = "CapitalQuestion";
            radioGroup.PartialName = "CapitalQuestion";
            // Allow no selection (optional)
            radioGroup.NoToggleToOff = false;
            // Set default appearance (font, size, color)
            radioGroup.DefaultAppearance = new DefaultAppearance("Helvetica", 10, System.Drawing.Color.Black);

            // Add options with their own rectangles (positioned below the question)
            // Option A
            radioGroup.AddOption("Paris", new Aspose.Pdf.Rectangle(70, 680, 90, 700));
            // Option B
            radioGroup.AddOption("London", new Aspose.Pdf.Rectangle(70, 660, 90, 680));
            // Option C
            radioGroup.AddOption("Berlin", new Aspose.Pdf.Rectangle(70, 640, 90, 660));
            // Option D
            radioGroup.AddOption("Madrid", new Aspose.Pdf.Rectangle(70, 620, 90, 640));

            // Add the radio button field to the document's form
            doc.Form.Add(radioGroup);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button form saved to '{outputPath}'.");
    }
}
