using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                if (absorber.TableList == null || absorber.TableList.Count == 0)
                {
                    Console.WriteLine("Page " + pageNumber + ": No tables found.");
                    continue;
                }

                for (int tableIndex = 0; tableIndex < absorber.TableList.Count; tableIndex++)
                {
                    for (int rowIndex = 0; rowIndex < absorber.TableList[tableIndex].RowList.Count; rowIndex++)
                    {
                        if (absorber.TableList[tableIndex].RowList[rowIndex].CellList.Count > 0 &&
                            absorber.TableList[tableIndex].RowList[rowIndex].CellList[0].TextFragments.Count > 0)
                        {
                            TextFragment identifierFragment = absorber.TableList[tableIndex].RowList[rowIndex].CellList[0].TextFragments[0];
                            string identifier = identifierFragment.Text.Trim();
                            Console.WriteLine("Page " + pageNumber + ": Row " + (rowIndex + 1) + " Identifier = " + identifier);
                        }
                    }
                }
            }
        }
    }
}