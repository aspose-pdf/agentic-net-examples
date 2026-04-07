using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace TableAbsorberExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------------------------------
            // Step 1: Create a sample PDF containing a simple table.
            // ------------------------------------------------------------
            using (Document createDoc = new Document())
            {
                Page page = createDoc.Pages.Add();
                Table table = new Table();
                // First row (header)
                Row headerRow = table.Rows.Add();
                Cell headerCell1 = headerRow.Cells.Add("Header 1");
                Cell headerCell2 = headerRow.Cells.Add("Header 2");
                // Second row (data)
                Row dataRow = table.Rows.Add();
                Cell dataCell1 = dataRow.Cells.Add("Value 1");
                Cell dataCell2 = dataRow.Cells.Add("Value 2");
                page.Paragraphs.Add(table);

                string inputPath = "input.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    createDoc.Save(inputPath);
                }
                else
                {
                    try
                    {
                        createDoc.Save(inputPath);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("GDI+ (libgdiplus) is not available on this platform – cannot save the sample PDF.");
                        return;
                    }
                }
            }

            // ------------------------------------------------------------
            // Step 2: Open the PDF and use TableAbsorber to locate tables.
            // ------------------------------------------------------------
            using (Document doc = new Document("input.pdf"))
            {
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(doc.Pages[1]); // page indexing is 1‑based

                if (absorber.TableList.Count > 0)
                {
                    AbsorbedTable absorbedTable = absorber.TableList[0];
                    // Access the first cell of the first row.
                    TextFragment fragment = absorbedTable.RowList[0].CellList[0].TextFragments[0];
                    Console.WriteLine("Original text: " + fragment.Text);
                    // Modify the text inside the cell.
                    fragment.Text = "Modified";
                    Console.WriteLine("Modified text: " + fragment.Text);
                }
                else
                {
                    Console.WriteLine("No tables were detected on the page.");
                }

                string outputPath = "output.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(outputPath);
                }
                else
                {
                    try
                    {
                        doc.Save(outputPath);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("GDI+ (libgdiplus) is not available on this platform – cannot save the modified PDF.");
                    }
                }
            }
        }

        private static bool ContainsDllNotFound(Exception ex)
        {
            Exception? current = ex;
            while (current != null)
            {
                if (current is DllNotFoundException)
                {
                    return true;
                }
                current = current.InnerException;
            }
            return false;
        }
    }
}
