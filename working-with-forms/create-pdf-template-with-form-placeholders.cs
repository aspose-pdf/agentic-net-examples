using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "TemplateWithPlaceholders.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a single page
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Add a title to the page
            Aspose.Pdf.Text.TextFragment title = new Aspose.Pdf.Text.TextFragment("Invoice Template");
            title.Position = new Aspose.Pdf.Text.Position(200, 750);
            title.TextState.FontSize = 20;
            title.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            title.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page.Paragraphs.Add(title);

            // ---------- Placeholder: Customer Name ----------
            // Label
            Aspose.Pdf.Text.TextFragment nameLabel = new Aspose.Pdf.Text.TextFragment("Customer Name:");
            nameLabel.Position = new Aspose.Pdf.Text.Position(50, 700);
            nameLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(nameLabel);

            // Text box field (placeholder)
            Aspose.Pdf.Forms.TextBoxField nameField = new Aspose.Pdf.Forms.TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(150, 690, 350, 710));
            nameField.PartialName = "CustomerName";
            nameField.Value = ""; // empty placeholder
            doc.Form.Add(nameField);

            // ---------- Placeholder: Invoice Date ----------
            Aspose.Pdf.Text.TextFragment dateLabel = new Aspose.Pdf.Text.TextFragment("Invoice Date:");
            dateLabel.Position = new Aspose.Pdf.Text.Position(50, 660);
            dateLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(dateLabel);

            Aspose.Pdf.Forms.TextBoxField dateField = new Aspose.Pdf.Forms.TextBoxField(
                page,
                new Aspose.Pdf.Rectangle(150, 650, 250, 670));
            dateField.PartialName = "InvoiceDate";
            dateField.Value = ""; // empty placeholder
            doc.Form.Add(dateField);

            // ---------- Placeholder: Paid (checkbox) ----------
            Aspose.Pdf.Text.TextFragment paidLabel = new Aspose.Pdf.Text.TextFragment("Paid:");
            paidLabel.Position = new Aspose.Pdf.Text.Position(50, 620);
            paidLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(paidLabel);

            Aspose.Pdf.Forms.CheckboxField paidCheck = new Aspose.Pdf.Forms.CheckboxField(
                page,
                new Aspose.Pdf.Rectangle(150, 610, 160, 620));
            paidCheck.PartialName = "IsPaid";
            paidCheck.Checked = false;
            doc.Form.Add(paidCheck);

            // ---------- Placeholder: Signature ----------
            Aspose.Pdf.Text.TextFragment sigLabel = new Aspose.Pdf.Text.TextFragment("Signature:");
            sigLabel.Position = new Aspose.Pdf.Text.Position(50, 580);
            sigLabel.TextState.FontSize = 12;
            page.Paragraphs.Add(sigLabel);

            Aspose.Pdf.Forms.SignatureField signature = new Aspose.Pdf.Forms.SignatureField(
                page,
                new Aspose.Pdf.Rectangle(150, 560, 350, 610));
            signature.PartialName = "CustomerSignature";
            doc.Form.Add(signature);

            // Save the template PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Template PDF created at '{outputPath}'.");
    }
}
