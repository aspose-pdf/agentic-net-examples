using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "AcroFormWithTooltips.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Helper method to create a text box field with a tooltip
            // -----------------------------------------------------------------
            TextBoxField CreateTextBox(Page p, double llx, double lly, double urx, double ury,
                                      string fieldName, string tooltip, string defaultText)
            {
                // Define the field rectangle (coordinates are in points)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the text box field on the specified page
                TextBoxField field = new TextBoxField(p, rect);

                // Set the partial name (used as the field identifier)
                field.Name = fieldName;

                // Set the tooltip (displayed as alternate name in Acrobat)
                field.AlternateName = tooltip;

                // Set an initial value (optional)
                field.Value = defaultText;

                // NOTE: The DefaultAppearance class may not be available in all
                // Aspose.PDF versions. It is not required for tooltip functionality,
                // so it has been omitted to avoid compilation errors.

                // Add the field to the document's form collection
                doc.Form.Add(field);

                return field;
            }

            // -----------------------------------------------------------------
            // Create sample fields with descriptive tooltips
            // -----------------------------------------------------------------
            // Name field
            CreateTextBox(page,
                          llx: 100, lly: 700, urx: 300, ury: 720,
                          fieldName: "FullName",
                          tooltip: "Enter your full name",
                          defaultText: "");

            // Email field
            CreateTextBox(page,
                          llx: 100, lly: 650, urx: 300, ury: 670,
                          fieldName: "EmailAddress",
                          tooltip: "Enter a valid email address",
                          defaultText: "");

            // Phone number field
            CreateTextBox(page,
                          llx: 100, lly: 600, urx: 300, ury: 620,
                          fieldName: "PhoneNumber",
                          tooltip: "Enter your phone number (e.g., +1-555-1234)",
                          defaultText: "");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}