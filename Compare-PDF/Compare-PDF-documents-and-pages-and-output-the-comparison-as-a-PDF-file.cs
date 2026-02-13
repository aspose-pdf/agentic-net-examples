using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfComparer
{
    static void Main(string[] args)
    {
        // Input PDF files and output comparison PDF
        string pdfPath1 = "input1.pdf";
        string pdfPath2 = "input2.pdf";
        string outputPath = "comparison.pdf";

        // Validate input files
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load source documents
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Create result document
        Document result = new Document();

        // Add a page for the report
        Page reportPage = result.Pages.Add();

        // Title
        TextFragment title = new TextFragment("PDF Comparison Report");
        title.TextState.FontSize = 18;
        title.TextState.FontStyle = FontStyles.Bold;
        title.Position = new Position(0, 800);
        reportPage.Paragraphs.Add(title);

        // Create a table: 4 columns (Metric, PDF1, PDF2, Equal)
        Table table = new Table
        {
            ColumnWidths = "150 150 150 80", // space‑separated widths
            DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
        };

        // Header row
        Row header = table.Rows.Add();
        header.Cells.Add(new TextFragment("Metric"));
        header.Cells.Add(new TextFragment("PDF 1"));
        header.Cells.Add(new TextFragment("PDF 2"));
        header.Cells.Add(new TextFragment("Equal?"));
        foreach (Cell c in header.Cells) c.DefaultCellTextState.FontStyle = FontStyles.Bold;

        // Helper to add a data row
        void AddRow(string metric, string val1, string val2, bool equal)
        {
            Row r = table.Rows.Add();
            r.Cells.Add(new TextFragment(metric));
            r.Cells.Add(new TextFragment(val1));
            r.Cells.Add(new TextFragment(val2));
            r.Cells.Add(new TextFragment(equal ? "Yes" : "No"));
        }

        // Compare page counts
        int count1 = doc1.Pages.Count;
        int count2 = doc2.Pages.Count;
        AddRow("Page Count", count1.ToString(), count2.ToString(), count1 == count2);

        // Compare each page size (width x height)
        int maxPages = Math.Max(count1, count2);
        for (int i = 1; i <= maxPages; i++)
        {
            string size1 = "N/A";
            string size2 = "N/A";
            bool equal = false;

            if (i <= count1)
            {
                Page p1 = doc1.Pages[i];
                size1 = $"{p1.PageInfo.Width:F2} x {p1.PageInfo.Height:F2}";
            }
            if (i <= count2)
            {
                Page p2 = doc2.Pages[i];
                size2 = $"{p2.PageInfo.Width:F2} x {p2.PageInfo.Height:F2}";
            }

            if (i <= count1 && i <= count2)
            {
                Page p1 = doc1.Pages[i];
                Page p2 = doc2.Pages[i];
                equal = Math.Abs(p1.PageInfo.Width - p2.PageInfo.Width) < 0.01 &&
                        Math.Abs(p1.PageInfo.Height - p2.PageInfo.Height) < 0.01;
            }

            AddRow($"Page {i} Size", size1, size2, equal);
        }

        // Add the table to the report page
        reportPage.Paragraphs.Add(table);

        // Save the comparison PDF
        result.Save(outputPath);
        Console.WriteLine($"Comparison PDF saved to: {outputPath}");
    }
}