using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfTableExample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output.pdf";
            const string finalPath = "final.pdf";

            // Step 1: Create a simple PDF file to work with
            using (Document createDoc = new Document())
            {
                createDoc.Pages.Add();
                SaveDocument(createDoc, inputPath);
            }

            // Step 2: Open the sample PDF and add a table
            using (Document doc = new Document(inputPath))
            {
                Table table = new Table();

                // First row
                Row firstRow = table.Rows.Add();
                Cell firstCell = firstRow.Cells.Add();
                firstCell.Paragraphs.Add(new TextFragment("Cell 1"));
                Cell secondCell = firstRow.Cells.Add();
                secondCell.Paragraphs.Add(new TextFragment("Cell 2"));

                // Second row
                Row secondRow = table.Rows.Add();
                Cell thirdCell = secondRow.Cells.Add();
                thirdCell.Paragraphs.Add(new TextFragment("Cell 3"));
                Cell fourthCell = secondRow.Cells.Add();
                fourthCell.Paragraphs.Add(new TextFragment("Cell 4"));

                // Add the table to the first page
                doc.Pages[1].Paragraphs.Add(table);
                SaveDocument(doc, outputPath);

                // Step 3: Extract the table and modify a cell's text using TableAbsorber
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(doc);

                if (absorber.TableList.Count > 0 &&
                    absorber.TableList[0].RowList.Count > 0 &&
                    absorber.TableList[0].RowList[0].CellList.Count > 0 &&
                    absorber.TableList[0].RowList[0].CellList[0].TextFragments.Count > 0)
                {
                    TextFragment fragment = absorber.TableList[0].RowList[0].CellList[0].TextFragments[0];
                    fragment.Text = "Modified";
                }

                SaveDocument(doc, finalPath);
            }
        }

        private static void SaveDocument(Document document, string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(path);
                Console.WriteLine($"PDF saved to '{path}'.");
            }
            else
            {
                try
                {
                    document.Save(path);
                    Console.WriteLine($"PDF saved to '{path}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine($"Warning: Unable to save '{path}' because GDI+ (libgdiplus) is not available on this platform.");
                }
            }
        }

        private static bool ContainsDllNotFound(Exception exception)
        {
            Exception current = exception;
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
