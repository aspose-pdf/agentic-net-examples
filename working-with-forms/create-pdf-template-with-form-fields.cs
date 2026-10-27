using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "template.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (first page)
            Page page = doc.Pages.Add();

            // ----- Create placeholder form fields -----

            // Name field
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            TextBoxField nameField = new TextBoxField(page, nameRect)
            {
                PartialName = "Name",
                Value = "«Name»",
                // Border property expects an Aspose.Pdf.Annotations.Border; omitted for simplicity
                Margin = new MarginInfo { Top = 2, Bottom = 2, Left = 2, Right = 2 }
            };
            doc.Form.Add(nameField);

            // Date field
            Aspose.Pdf.Rectangle dateRect = new Aspose.Pdf.Rectangle(100, 650, 300, 670);
            TextBoxField dateField = new TextBoxField(page, dateRect)
            {
                PartialName = "Date",
                Value = "«Date»",
                Margin = new MarginInfo { Top = 2, Bottom = 2, Left = 2, Right = 2 }
            };
            doc.Form.Add(dateField);

            // Amount field
            Aspose.Pdf.Rectangle amountRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField amountField = new TextBoxField(page, amountRect)
            {
                PartialName = "Amount",
                Value = "«Amount»",
                Margin = new MarginInfo { Top = 2, Bottom = 2, Left = 2, Right = 2 }
            };
            doc.Form.Add(amountField);

            // ----- Add labels for the fields -----

            TextFragment nameLabel = new TextFragment("Name:")
            {
                Position = new Position(50, 710),
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") }
            };
            page.Paragraphs.Add(nameLabel);

            TextFragment dateLabel = new TextFragment("Date:")
            {
                Position = new Position(50, 660),
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") }
            };
            page.Paragraphs.Add(dateLabel);

            TextFragment amountLabel = new TextFragment("Amount:")
            {
                Position = new Position(50, 610),
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial") }
            };
            page.Paragraphs.Add(amountLabel);

            // Save the PDF template
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF template created at '{outputPath}'.");
    }
}
