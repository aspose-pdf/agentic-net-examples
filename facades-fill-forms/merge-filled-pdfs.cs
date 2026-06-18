using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple PDF template with a form field.
        using (Aspose.Pdf.Document templateDoc = new Aspose.Pdf.Document())
        {
            Aspose.Pdf.Page templatePage = templateDoc.Pages.Add();
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            Aspose.Pdf.Forms.TextBoxField nameField = new Aspose.Pdf.Forms.TextBoxField(templatePage, fieldRect);
            nameField.PartialName = "Name";
            templateDoc.Form.Add(nameField);
            templateDoc.Save("template.pdf");
        }

        // Step 2: Prepare two DataTables with data for the form.
        System.Data.DataTable table1 = new System.Data.DataTable();
        table1.Columns.Add("Name", typeof(string));
        table1.Rows.Add("Alice");

        System.Data.DataTable table2 = new System.Data.DataTable();
        table2.Columns.Add("Name", typeof(string));
        table2.Rows.Add("Bob");

        // Step 3: Fill the template with each DataTable producing individual PDFs.
        FillTemplateWithData("template.pdf", table1, "filled1.pdf");
        FillTemplateWithData("template.pdf", table2, "filled2.pdf");

        // Step 4: Merge the filled PDFs into a single document.
        using (Aspose.Pdf.Document mergedDoc = new Aspose.Pdf.Document())
        {
            mergedDoc.Merge("filled1.pdf", "filled2.pdf");
            mergedDoc.Save("merged.pdf");
        }

        Console.WriteLine("Merged PDF created successfully.");
    }

    private static void FillTemplateWithData(string templatePath, System.Data.DataTable data, string outputPath)
    {
        Aspose.Pdf.Facades.AutoFiller filler = new Aspose.Pdf.Facades.AutoFiller();
        filler.InputFileName = templatePath;
        filler.ImportDataTable(data);
        filler.Save(outputPath);
        filler.Close();
    }
}