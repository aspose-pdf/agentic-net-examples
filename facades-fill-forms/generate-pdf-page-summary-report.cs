using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string reportPath = "summary_report.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Example DataTable containing row identifiers (replace with real data as needed)
            DataTable dt = GetSampleDataTable();

            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine("PageNumber\tRowIdentifier");

                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Extract tables from the current page
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page);

                    string identifier = "N/A";

                    if (absorber.TableList.Count > 0)
                    {
                        // Assume the identifier is in the first cell of the first row
                        var firstCell = absorber.TableList[0].RowList[0].CellList[0];
                        if (firstCell.TextFragments.Count > 0)
                        {
                            identifier = firstCell.TextFragments[0].Text.Trim();
                        }
                    }

                    // Optional: match the extracted identifier with a DataTable row
                    DataRow[] matchingRows = dt.Select($"Id = '{identifier}'");
                    if (matchingRows.Length > 0)
                    {
                        identifier = matchingRows[0]["Id"].ToString();
                    }

                    writer.WriteLine($"{i}\t{identifier}");
                }
            }

            Console.WriteLine($"Summary report saved to '{reportPath}'.");
        }
    }

    // Creates a sample DataTable with an Id column (replace with actual data source)
    static DataTable GetSampleDataTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Id", typeof(string));
        table.Rows.Add("A001");
        table.Rows.Add("A002");
        table.Rows.Add("A003");
        return table;
    }
}