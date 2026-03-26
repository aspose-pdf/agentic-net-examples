using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        string outputPath = "dropdown_form.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangle for combo box (lower-left x, lower-left y, upper-right x, upper-right y)
            Rectangle rect = new Rectangle(100.0, 500.0, 300.0, 530.0);

            // Create combo box field
            ComboBoxField combo = new ComboBoxField(page, rect);
            combo.PartialName = "SampleCombo";

            // Populate options from array
            string[] items = new string[] { "Apple", "Banana", "Cherry", "Date" };
            foreach (string item in items)
            {
                combo.AddOption(item);
            }

            // Add combo box to the document form
            doc.Form.Add(combo);

            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with dropdown list created.");
    }
}
