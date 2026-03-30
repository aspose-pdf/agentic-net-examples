using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

public class Program
{
    public static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a simple PDF template with form fields (Name, Address).
        //    This removes the external file dependency that caused the
        //    FileNotFoundException in the original code.
        // ---------------------------------------------------------------------
        string templatePath = Path.GetFullPath("template.pdf");
        CreateTemplatePdf(templatePath);

        // ---------------------------------------------------------------------
        // 2. Prepare two DataTables with sample data.
        // ---------------------------------------------------------------------
        DataTable dataTable1 = new DataTable("Table1");
        dataTable1.Columns.Add("Name", typeof(string));
        dataTable1.Columns.Add("Address", typeof(string));
        DataRow dataRow1 = dataTable1.NewRow();
        dataRow1["Name"] = "John Doe";
        dataRow1["Address"] = "123 Main St";
        dataTable1.Rows.Add(dataRow1);

        DataTable dataTable2 = new DataTable("Table2");
        dataTable2.Columns.Add("Name", typeof(string));
        dataTable2.Columns.Add("Address", typeof(string));
        DataRow dataRow2 = dataTable2.NewRow();
        dataRow2["Name"] = "Jane Smith";
        dataRow2["Address"] = "456 Oak Ave";
        dataTable2.Rows.Add(dataRow2);

        // ---------------------------------------------------------------------
        // 3. Fill the template with each DataTable and save the intermediate PDFs.
        // ---------------------------------------------------------------------
        string filledFileName1 = "filled0.pdf";
        FillTemplate(templatePath, dataTable1, filledFileName1);

        string filledFileName2 = "filled1.pdf";
        FillTemplate(templatePath, dataTable2, filledFileName2);

        // ---------------------------------------------------------------------
        // 4. Merge the two filled PDFs into a single document.
        // ---------------------------------------------------------------------
        string mergedPath = "merged.pdf";
        MergePdfs(new[] { filledFileName1, filledFileName2 }, mergedPath);

        // ---------------------------------------------------------------------
        // 5. Clean‑up temporary files.
        // ---------------------------------------------------------------------
        File.Delete(templatePath);
        File.Delete(filledFileName1);
        File.Delete(filledFileName2);
    }

    private static void CreateTemplatePdf(string path)
    {
        // Create a blank PDF with two text box form fields: Name and Address.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Name field
        TextBoxField nameField = new TextBoxField(page, new Rectangle(100, 700, 300, 720))
        {
            PartialName = "Name",
            Value = string.Empty
        };
        doc.Form.Add(nameField);

        // Address field
        TextBoxField addressField = new TextBoxField(page, new Rectangle(100, 660, 300, 680))
        {
            PartialName = "Address",
            Value = string.Empty
        };
        doc.Form.Add(addressField);

        doc.Save(path);
    }

    private static void FillTemplate(string templatePath, DataTable data, string outputPath)
    {
        // AutoFiller binds the template, imports the DataTable and saves the result.
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);
            filler.ImportDataTable(data);
            filler.Save(outputPath);
        }
    }

    private static void MergePdfs(string[] sourceFiles, string outputPath)
    {
        // Create a new empty document that will hold the merged result.
        Document mergedDoc = new Document();

        foreach (string file in sourceFiles)
        {
            // Load each source PDF.
            Document srcDoc = new Document(file);
            // Append all pages of the source document to the merged document.
            mergedDoc.Pages.Add(srcDoc.Pages);
        }

        mergedDoc.Save(outputPath);
    }
}
