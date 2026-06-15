using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with a checkbox field
        using (Aspose.Pdf.Document document = new Aspose.Pdf.Document())
        {
            Aspose.Pdf.Page page = document.Pages.Add();

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);
            Aspose.Pdf.Forms.CheckboxField checkbox = new Aspose.Pdf.Forms.CheckboxField(page, rect);
            checkbox.PartialName = "AgreeTerms";
            checkbox.ExportValue = "Yes";
            checkbox.Checked = true;

            document.Form.Add(checkbox);
            document.Save("sample.pdf");
        }

        // Export the checkbox field definitions to an FDF file
        Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form("sample.pdf");
        using (FileStream fdfStream = new FileStream("checkboxes.fdf", FileMode.Create, FileAccess.Write))
        {
            form.ExportFdf(fdfStream);
        }
    }
}