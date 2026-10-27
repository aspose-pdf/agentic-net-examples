using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table_field.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page as the container for the form field
            Page page = doc.Pages[1];

            // Define the rectangle for the TextBoxField (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the TextBoxField and assign a name
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "TableField",
                Value = string.Empty // initial value
            };

            // Create a temporary page that will serve as the appearance stream for the normal state
            Page appearancePage = doc.Pages.Add();

            // Build a simple table to be displayed inside the field
            Table table = new Table
            {
                // Optional: set column widths (two columns of equal width)
                ColumnWidths = "200 200"
            };

            // Header row
            Row header = table.Rows.Add();
            Cell hCell1 = header.Cells.Add();
            hCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell hCell2 = header.Cells.Add();
            hCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // Data row
            Row data = table.Rows.Add();
            Cell dCell1 = data.Cells.Add();
            dCell1.Paragraphs.Add(new TextFragment("Cell A"));
            Cell dCell2 = data.Cells.Add();
            dCell2.Paragraphs.Add(new TextFragment("Cell B"));

            // Add the table to the temporary appearance page
            appearancePage.Paragraphs.Add(table);

            // Register the appearance for the textbox field using the Form API (no direct NormalAppearance property)
            doc.Form.AddFieldAppearance(txtField, appearancePage.Number, fieldRect);

            // Add the textbox field (with its appearance) to the page annotations
            page.Annotations.Add(txtField);

            // Remove the temporary appearance page from the document so it does not appear as a separate page
            doc.Pages.Delete(doc.Pages.Count);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table inside form field: {outputPath}");
    }
}
