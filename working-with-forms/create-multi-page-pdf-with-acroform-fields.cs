using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add three pages; each page will receive its own distinct form fields
            for (int i = 1; i <= 3; i++)
            {
                // Add a blank page
                Page page = doc.Pages.Add();

                // ----- TextBox field (unique per page) -----
                // Define the rectangle for the field on the current page
                Rectangle txtRect = new Rectangle(100, 700, 300, 730);
                // Use the constructor that takes the page and rectangle – this binds the field to the page
                TextBoxField txt = new TextBoxField(page, txtRect);
                txt.PartialName = $"txtPage{i}";
                txt.Value = $"Text on page {i}";
                // Add the field to the document's AcroForm collection
                doc.Form.Add(txt);

                // ----- CheckBox field (unique per page) -----
                Rectangle chkRect = new Rectangle(100, 650, 115, 665);
                CheckboxField chk = new CheckboxField(page, chkRect);
                chk.PartialName = $"chkPage{i}";
                chk.Checked = (i % 2 == 0); // checked on even pages, unchecked on odd
                doc.Form.Add(chk);
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with distinct AcroForm fields saved to '{outputPath}'.");
    }
}
