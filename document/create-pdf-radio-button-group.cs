using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;               // TextFragment
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input/Output paths
        const string outputPath = "quiz_form.pdf";

        // Ensure output directory exists (handle possible null from GetDirectoryName)
        string outputDir = Path.GetDirectoryName(outputPath) ?? string.Empty;
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Question 1: "What is the capital of France?"
            // -----------------------------------------------------------------
            // Add the question text
            TextFragment q1Text = new TextFragment("1. What is the capital of France?");
            q1Text.Position = new Aspose.Pdf.Text.Position(50, 750);
            q1Text.TextState.FontSize = 12;
            page.Paragraphs.Add(q1Text);

            // Create a radio‑button group for Question 1
            RadioButtonField q1Group = new RadioButtonField(page) { PartialName = "Q1" };

            // Add the individual options (RadioButtonOptionField) – each option has its own rectangle
            RadioButtonOptionField q1Opt1 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 720, 55, 725)) { OptionName = "Paris" };
            RadioButtonOptionField q1Opt2 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 700, 55, 705)) { OptionName = "London" };
            RadioButtonOptionField q1Opt3 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 680, 55, 685)) { OptionName = "Berlin" };
            RadioButtonOptionField q1Opt4 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 660, 55, 665)) { OptionName = "Madrid" };

            // Add options to the group (order matters)
            q1Group.Add(q1Opt1);
            q1Group.Add(q1Opt2);
            q1Group.Add(q1Opt3);
            q1Group.Add(q1Opt4);

            // Register the group with the document form
            doc.Form.Add(q1Group, 1);

            // Set default selected value (Paris – first option)
            q1Group.Selected = 1; // 1‑based index

            // -----------------------------------------------------------------
            // Question 2: "Which language is primarily used for Android development?"
            // -----------------------------------------------------------------
            TextFragment q2Text = new TextFragment("2. Which language is primarily used for Android development?");
            q2Text.Position = new Aspose.Pdf.Text.Position(50, 620);
            q2Text.TextState.FontSize = 12;
            page.Paragraphs.Add(q2Text);

            RadioButtonField q2Group = new RadioButtonField(page) { PartialName = "Q2" };

            RadioButtonOptionField q2Opt1 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 590, 55, 595)) { OptionName = "Java" };
            RadioButtonOptionField q2Opt2 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 570, 55, 575)) { OptionName = "Kotlin" };
            RadioButtonOptionField q2Opt3 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 550, 55, 555)) { OptionName = "Swift" };
            RadioButtonOptionField q2Opt4 = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 530, 55, 535)) { OptionName = "C#" };

            q2Group.Add(q2Opt1);
            q2Group.Add(q2Opt2);
            q2Group.Add(q2Opt3);
            q2Group.Add(q2Opt4);

            // Register the second group (no default selection)
            doc.Form.Add(q2Group, 1);

            // -----------------------------------------------------------------
            // Save the PDF (save options not required for PDF format)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with radio buttons saved to '{outputPath}'.");
    }
}
