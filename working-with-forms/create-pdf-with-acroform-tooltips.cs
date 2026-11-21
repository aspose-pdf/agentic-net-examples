using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTooltips.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Load a font for field labels
            Aspose.Pdf.Text.Font font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");

            // ----- Name field -----
            // Label
            Aspose.Pdf.Text.TextFragment nameLabel = new Aspose.Pdf.Text.TextFragment("Name:");
            nameLabel.Position = new Aspose.Pdf.Text.Position(50, 750);
            nameLabel.TextState.Font = font;
            nameLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(nameLabel);

            // TextBox field
            Aspose.Pdf.Forms.TextBoxField nameField = new Aspose.Pdf.Forms.TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(150, 735, 350, 755));
            nameField.PartialName = "NameField";
            nameField.Value = "";
            // Use AlternateName for tooltip (Tooltip property does not exist)
            nameField.AlternateName = "Enter the full name of the person.";
            doc.Form.Add(nameField, 1);

            // ----- Age field -----
            // Label
            Aspose.Pdf.Text.TextFragment ageLabel = new Aspose.Pdf.Text.TextFragment("Age:");
            ageLabel.Position = new Aspose.Pdf.Text.Position(50, 710);
            ageLabel.TextState.Font = font;
            ageLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(ageLabel);

            // TextBox field
            Aspose.Pdf.Forms.TextBoxField ageField = new Aspose.Pdf.Forms.TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(150, 695, 200, 715));
            ageField.PartialName = "AgeField";
            ageField.Value = "";
            ageField.AlternateName = "Enter the age in years.";
            doc.Form.Add(ageField, 1);

            // ----- Subscribe checkbox -----
            // Label
            Aspose.Pdf.Text.TextFragment subscribeLabel = new Aspose.Pdf.Text.TextFragment("Subscribe to newsletter:");
            subscribeLabel.Position = new Aspose.Pdf.Text.Position(50, 670);
            subscribeLabel.TextState.Font = font;
            subscribeLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(subscribeLabel);

            // CheckBox field (correct class name is CheckboxField)
            Aspose.Pdf.Forms.CheckboxField subscribeCheck = new Aspose.Pdf.Forms.CheckboxField(
                page,
                new Aspose.Pdf.Rectangle(250, 655, 265, 670));
            subscribeCheck.PartialName = "SubscribeCheck";
            subscribeCheck.Checked = false;
            subscribeCheck.AlternateName = "Check to receive the newsletter.";
            doc.Form.Add(subscribeCheck, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
