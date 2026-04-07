using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
// using Aspose.Pdf.Drawing; // Not needed for this sample – removed to avoid ambiguity

class PdfTemplateCreator
{
    static void Main()
    {
        const string outputPath = "TemplateWithPlaceholders.pdf";

        // Create an empty PDF document (use the public constructor – DocumentFactory.CreateDocument() is non‑static in recent versions)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Add a title text fragment
            TextFragment title = new TextFragment("Invoice Template")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(0, 800)
            };
            // TextState is read‑only – modify its members instead of assigning a new instance
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.TextState.FontSize = 20;
            title.TextState.ForegroundColor = Color.DarkBlue;
            page.Paragraphs.Add(title);

            // Helper to create a textbox field placeholder
            TextBoxField CreatePlaceholder(string fieldName, double llx, double lly, double urx, double ury)
            {
                // Use the Aspose.Pdf.Rectangle (annotation rectangle) – fully qualified to avoid ambiguity
                var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // The constructor that receives the Page attaches the field to the correct page
                TextBoxField field = new TextBoxField(page, rect)
                {
                    PartialName = fieldName,
                    // Empty default value – placeholder
                    Value = string.Empty
                };
                // Add the field to the document's form collection
                doc.Form.Add(field);
                return field;
            }

            // Add placeholder fields (example: Customer Name, Invoice Date, Total Amount)
            CreatePlaceholder("CustomerName", 100, 700, 300, 720);
            CreatePlaceholder("InvoiceDate", 100, 660, 300, 680);
            CreatePlaceholder("TotalAmount", 100, 620, 300, 640);

            // Optionally add labels next to the fields for visual guidance
            void AddLabel(string text, double x, double y)
            {
                TextFragment label = new TextFragment(text)
                {
                    Position = new Position(x, y)
                };
                label.TextState.Font = FontRepository.FindFont("Helvetica");
                label.TextState.FontSize = 12;
                label.TextState.ForegroundColor = Color.Black;
                page.Paragraphs.Add(label);
            }

            AddLabel("Customer Name:", 50, 710);
            AddLabel("Invoice Date:", 50, 670);
            AddLabel("Total Amount:", 50, 630);

            // Save the template PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF template with placeholders saved to '{outputPath}'.");
    }
}
