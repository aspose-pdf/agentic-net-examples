using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Create a sample PDF file (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
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
                    Console.WriteLine("Skipping save of the sample PDF because GDI+ (libgdiplus) is not available on this platform.");
                }
            }
        }

        // Open the existing PDF and insert a formatted table
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            // Position the table on the page
            table.Left = 50f;
            table.Top = 700f;
            // Define two equal columns
            table.ColumnWidths = "200 200";
            // Set borders for the table and its cells
            Aspose.Pdf.BorderInfo border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 1f);
            table.Border = border;
            table.DefaultCellBorder = border;
            // Set default text style for cells (avoid GDI+ font loading on non‑Windows platforms)
            Aspose.Pdf.Text.TextState defaultTextState = new Aspose.Pdf.Text.TextState();
            defaultTextState.FontSize = 12f;
            // Do not set Font explicitly to keep the example platform‑agnostic
            table.DefaultCellTextState = defaultTextState;

            // Add header row (first row)
            Aspose.Pdf.Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Product");
            headerRow.Cells.Add("Price");

            // Add data row (second row)
            Aspose.Pdf.Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Apple");
            dataRow.Cells.Add("$1.00");

            // Append the table to the first page (1‑based indexing)
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF with platform guard
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
                    Console.WriteLine("Cannot save the PDF on this platform because GDI+ (libgdiplus) is missing.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception current = ex;
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