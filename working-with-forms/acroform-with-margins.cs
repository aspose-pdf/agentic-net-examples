using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for Position and TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Set explicit margins (left, right, top, bottom) in points using MarginInfo
            page.PageInfo.Margin = new MarginInfo { Left = 50, Right = 50, Top = 50, Bottom = 50 };

            // Compute the usable rectangle inside the margins
            double left   = page.PageInfo.Margin.Left;
            double right  = page.PageInfo.Width - page.PageInfo.Margin.Right;
            double top    = page.PageInfo.Height - page.PageInfo.Margin.Top;
            double bottom = page.PageInfo.Margin.Bottom;

            // Define field dimensions and spacing
            double fieldWidth      = 200;
            double fieldHeight     = 20;
            double verticalSpacing = 30;

            // ---- First AcroForm field: TextBox for "Name" ----
            // Position is relative to the top‑margin (Y decreases downwards)
            TextBoxField nameField = new TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(
                    left,                     // lower‑left X
                    top - fieldHeight,        // lower‑left Y
                    left + fieldWidth,        // upper‑right X
                    top));                    // upper‑right Y
            nameField.PartialName = "Name";
            nameField.Value = "Enter name";
            doc.Form.Add(nameField);

            // ---- Second AcroForm field: TextBox for "Email" ----
            double secondTop = top - fieldHeight - verticalSpacing;
            TextBoxField emailField = new TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(
                    left,
                    secondTop - fieldHeight,
                    left + fieldWidth,
                    secondTop));
            emailField.PartialName = "Email";
            emailField.Value = "Enter email";
            doc.Form.Add(emailField);

            // ---- Third AcroForm field: CheckBox for "Subscribe" ----
            double thirdTop = secondTop - fieldHeight - verticalSpacing;
            CheckboxField subscribeField = new CheckboxField(
                page,
                new Aspose.Pdf.Rectangle(
                    left,
                    thirdTop - fieldHeight,
                    left + 20,   // small square for the box
                    thirdTop));
            subscribeField.PartialName = "Subscribe";
            subscribeField.Checked = false;
            doc.Form.Add(subscribeField);

            // Add a label next to the checkbox
            TextFragment label = new TextFragment("Subscribe to newsletter");
            // Position uses Aspose.Pdf.Text.Position (X,Y) where Y is baseline
            label.Position = new Position(left + 25, thirdTop - fieldHeight / 2);
            page.Paragraphs.Add(label);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
