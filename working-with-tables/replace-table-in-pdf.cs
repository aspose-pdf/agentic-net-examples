using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Iterate over each page to locate and replace tables
            foreach (Aspose.Pdf.Page page in doc.Pages)
            {
                Aspose.Pdf.Table oldTable = null;
                int oldIndex = -1;

                // Find the first Table in the page's Paragraphs collection
                for (int i = 0; i < page.Paragraphs.Count; i++)
                {
                    if (page.Paragraphs[i] is Aspose.Pdf.Table tbl)
                    {
                        oldTable = tbl;
                        oldIndex = i;
                        break;
                    }
                }

                // If a table was found, replace it
                if (oldTable != null && oldIndex >= 0)
                {
                    // Remove the existing table
                    page.Paragraphs.Remove(oldTable);

                    // Create a new table (example: 2 columns, 2 rows)
                    Aspose.Pdf.Table newTable = new Aspose.Pdf.Table();
                    newTable.ColumnWidths = "200 200";

                    // First row
                    Aspose.Pdf.Row row1 = new Aspose.Pdf.Row();
                    Aspose.Pdf.Cell cell11 = new Aspose.Pdf.Cell();
                    cell11.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("New Cell 1"));
                    Aspose.Pdf.Cell cell12 = new Aspose.Pdf.Cell();
                    cell12.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("New Cell 2"));
                    row1.Cells.Add(cell11);
                    row1.Cells.Add(cell12);
                    newTable.Rows.Add(row1);

                    // Second row
                    Aspose.Pdf.Row row2 = new Aspose.Pdf.Row();
                    Aspose.Pdf.Cell cell21 = new Aspose.Pdf.Cell();
                    cell21.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("New Cell 3"));
                    Aspose.Pdf.Cell cell22 = new Aspose.Pdf.Cell();
                    cell22.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("New Cell 4"));
                    row2.Cells.Add(cell21);
                    row2.Cells.Add(cell22);
                    newTable.Rows.Add(row2);

                    // Insert the new table at the original position
                    page.Paragraphs.Insert(oldIndex, newTable);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table replaced and saved to '{outputPath}'.");
    }
}