using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // required by task specification

class SummaryReportGenerator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportCsvPath = "PageSummaryReport.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Example DataTable containing row identifiers.
        // In a real scenario this would be supplied by the caller.
        DataTable identifierTable = CreateSampleIdentifierTable();

        // Open the PDF document inside a using block (document disposal rule).
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a StringWriter to build CSV content.
            using (StringWriter csvWriter = new StringWriter())
            {
                // Write CSV header.
                csvWriter.WriteLine("PageNumber,RowIdentifier");

                // Iterate pages using 1‑based indexing (page-indexing-one-based rule).
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Extract tables from the current page (TableAbsorber from Aspose.Pdf.Text).
                    TableAbsorber tableAbsorber = new TableAbsorber();
                    tableAbsorber.Visit(page);

                    string rowId = "N/A";

                    // If a table is found, attempt to read an identifier from the first cell.
                    if (tableAbsorber.TableList.Count > 0 &&
                        tableAbsorber.TableList[0].RowList.Count > 0 &&
                        tableAbsorber.TableList[0].RowList[0].CellList.Count > 0 &&
                        tableAbsorber.TableList[0].RowList[0].CellList[0].TextFragments.Count > 0)
                    {
                        // Grab the text of the first fragment in the first cell.
                        TextFragment fragment = tableAbsorber.TableList[0].RowList[0].CellList[0].TextFragments[0];
                        rowId = fragment.Text.Trim();
                    }
                    else
                    {
                        // Fallback: try to match the page number with a row in the supplied DataTable.
                        // Assumes the DataTable has a column named "PageNumber".
                        DataRow[] matches = identifierTable.Select($"PageNumber = {pageNum}");
                        if (matches.Length > 0 && identifierTable.Columns.Contains("Identifier"))
                        {
                            rowId = matches[0]["Identifier"].ToString();
                        }
                    }

                    // Write the mapping to CSV.
                    csvWriter.WriteLine($"{pageNum},{EscapeCsv(rowId)}");
                }

                // Save the CSV report to disk.
                File.WriteAllText(reportCsvPath, csvWriter.ToString());
                Console.WriteLine($"Summary report saved to '{reportCsvPath}'.");
            }

            // No modifications to the PDF are required, but if you need to save a copy:
            // pdfDoc.Save("output_copy.pdf"); // example of using the save rule
        }
    }

    // Helper to create a sample DataTable with PageNumber ↔ Identifier mapping.
    private static DataTable CreateSampleIdentifierTable()
    {
        DataTable dt = new DataTable("PageIdentifiers");
        dt.Columns.Add("PageNumber", typeof(int));
        dt.Columns.Add("Identifier", typeof(string));

        // Sample data – replace with real data as needed.
        dt.Rows.Add(1, "INV-1001");
        dt.Rows.Add(2, "INV-1002");
        dt.Rows.Add(3, "INV-1003");
        // Add more rows as required.

        return dt;
    }

    // CSV escaping for values that may contain commas or quotes.
    private static string EscapeCsv(string value)
    {
        if (value == null) return "";
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            string escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return value;
    }
}