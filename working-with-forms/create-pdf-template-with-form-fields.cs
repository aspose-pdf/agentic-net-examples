using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // Required for Border and DefaultAppearance

class Program
{
    static void Main()
    {
        const string outputPath = "template.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Add a label for the name field
            TextFragment nameLabel = new TextFragment("Name:");
            nameLabel.Position = new Position(50, 750);
            nameLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(nameLabel);

            // Create a textbox placeholder for the name
            TextBoxField nameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(150, 735, 350, 755));
            nameField.PartialName = "NamePlaceholder"; // field identifier
            nameField.Value = ""; // empty placeholder value
            // Set appearance (font name, size, color) via DefaultAppearance
            nameField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            // Set border after the field instance is created (Border requires the parent annotation)
            nameField.Border = new Border(nameField) { Width = 1 };
            doc.Form.Add(nameField, 1); // add to the form on page 1

            // Add a label for the date field
            TextFragment dateLabel = new TextFragment("Date:");
            dateLabel.Position = new Position(50, 700);
            dateLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(dateLabel);

            // Create a textbox placeholder for the date
            TextBoxField dateField = new TextBoxField(page, new Aspose.Pdf.Rectangle(150, 685, 250, 705));
            dateField.PartialName = "DatePlaceholder";
            dateField.Value = "";
            // Set appearance via DefaultAppearance
            dateField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            dateField.Border = new Border(dateField) { Width = 1 };
            doc.Form.Add(dateField, 1);

            // Save the PDF template
            doc.Save(outputPath);
        }

        Console.WriteLine($"Template PDF created at '{outputPath}'.");
    }
}
