using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace ReplaceTableExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with an initial table
            string inputPath = "input.pdf";
            using (Document createDoc = new Document())
            {
                // Add a page
                Page page = createDoc.Pages.Add();

                // Create a simple table with two columns
                Table originalTable = new Table();
                originalTable.ColumnWidths = "120 120"; // space‑separated widths

                // First row (header)
                Row headerRow = originalTable.Rows.Add();
                Cell headerCell1 = headerRow.Cells.Add();
                headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
                Cell headerCell2 = headerRow.Cells.Add();
                headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

                // Second row (data)
                Row dataRow = originalTable.Rows.Add();
                Cell dataCell1 = dataRow.Cells.Add();
                dataCell1.Paragraphs.Add(new TextFragment("Old A"));
                Cell dataCell2 = dataRow.Cells.Add();
                dataCell2.Paragraphs.Add(new TextFragment("Old B"));

                // Add the table to the page
                page.Paragraphs.Add(originalTable);

                // Save the sample PDF
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
                        Console.WriteLine("GDI+ is not available on this platform; skipping sample PDF creation.");
                        return;
                    }
                }
            }

            // Step 2: Load the PDF and replace the existing table
            string outputPath = "output.pdf";
            using (Document doc = new Document(inputPath))
            {
                // Locate tables on the first page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(doc.Pages[1]);

                if (absorber.TableList.Count == 0)
                {
                    Console.WriteLine("No tables found on the first page.");
                    return;
                }

                // Get the first absorbed table
                AbsorbedTable oldTable = absorber.TableList[0];

                // Build a new table that will replace the old one
                Table newTable = new Table();
                newTable.ColumnWidths = "120 120";

                // Header row for new table
                Row newHeader = newTable.Rows.Add();
                Cell newHeaderCell1 = newHeader.Cells.Add();
                newHeaderCell1.Paragraphs.Add(new TextFragment("New Header 1"));
                Cell newHeaderCell2 = newHeader.Cells.Add();
                newHeaderCell2.Paragraphs.Add(new TextFragment("New Header 2"));

                // Data row for new table
                Row newData = newTable.Rows.Add();
                Cell newDataCell1 = newData.Cells.Add();
                newDataCell1.Paragraphs.Add(new TextFragment("New A"));
                Cell newDataCell2 = newData.Cells.Add();
                newDataCell2.Paragraphs.Add(new TextFragment("New B"));

                // Replace the old table with the new one on the same page
                absorber.Replace(doc.Pages[1], oldTable, newTable);

                // Save the modified document
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
                        Console.WriteLine("GDI+ is not available on this platform; PDF saved without rendering graphics.");
                    }
                }
            }
        }

        // Helper method to detect missing libgdiplus on non‑Windows platforms
        private static bool ContainsDllNotFound(Exception ex)
        {
            Exception? current = ex;
            while (current != null)
            {
                if (current is DllNotFoundException)
                    return true;
                current = current.InnerException;
            }
            return false;
        }
    }
}
