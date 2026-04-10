using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string reportPath = "summary_report.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Example DataTable with an identifier column.
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(string));
        for (int i = 1; i <= 100; i++)
        {
            dt.Rows.Add($"Row{i}");
        }

        using (Document doc = new Document(pdfPath))
        {
            // Prepare CSV output.
            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine("PageNumber,DataTableRowID");

                // Iterate pages (1‑based indexing).
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Extract tables from the current page.
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(doc.Pages[pageNum]);

                    if (absorber.TableList.Count > 0)
                    {
                        // Map page number to a DataTable row identifier.
                        int rowIndex = pageNum - 1; // zero‑based index into DataTable.
                        string rowId = rowIndex < dt.Rows.Count
                            ? dt.Rows[rowIndex]["ID"].ToString()
                            : string.Empty;

                        writer.WriteLine($"{pageNum},{rowId}");
                    }
                    else
                    {
                        // No table on this page.
                        writer.WriteLine($"{pageNum},");
                    }
                }
            }

            // Demonstrate usage of a Facade class (PdfExtractor) as required.
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(doc);
            extractor.Close();
        }

        Console.WriteLine($"Summary report saved to '{reportPath}'.");
    }
}