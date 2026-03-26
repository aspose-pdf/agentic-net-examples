using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField checkbox = new CheckboxField(page, rect);
            checkbox.Name = "SampleCheckbox";
            checkbox.Checked = true;

            doc.Form.Add(checkbox);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox PDF saved to '{outputPath}'.");
    }
}