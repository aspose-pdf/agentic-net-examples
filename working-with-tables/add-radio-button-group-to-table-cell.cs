using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "radio_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "200 200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // First cell: label text
            Cell labelCell = row.Cells.Add();
            labelCell.Paragraphs.Add(new TextFragment("Choose an option:"));

            // Second cell: will contain the radio button group
            Cell radioCell = row.Cells.Add();

            // Create a radio button field that will act as the group container
            RadioButtonField radioGroup = new RadioButtonField(doc)
            {
                PartialName = "OptionGroup" // All options with this name belong to the same group
            };

            // ----- Option 1 -----
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(0, 0, 20, 20);
            RadioButtonOptionField opt1 = new RadioButtonOptionField(page, rect1)
            {
                OptionName = "Option1",
                Caption = new TextFragment("Option 1")
            };
            radioGroup.Add(opt1);               // Add to the group
            radioCell.Paragraphs.Add(opt1);     // Place visual representation in the cell

            // ----- Option 2 -----
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(0, 30, 20, 50);
            RadioButtonOptionField opt2 = new RadioButtonOptionField(page, rect2)
            {
                OptionName = "Option2",
                Caption = new TextFragment("Option 2")
            };
            radioGroup.Add(opt2);
            radioCell.Paragraphs.Add(opt2);

            // ----- Option 3 -----
            Aspose.Pdf.Rectangle rect3 = new Aspose.Pdf.Rectangle(0, 60, 20, 80);
            RadioButtonOptionField opt3 = new RadioButtonOptionField(page, rect3)
            {
                OptionName = "Option3",
                Caption = new TextFragment("Option 3")
            };
            radioGroup.Add(opt3);
            radioCell.Paragraphs.Add(opt3);

            // Register the radio button group with the document's form collection
            doc.Form.Add(radioGroup);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(outputPath);
                }
                else
                {
                    // Attempt to save; if libgdiplus is missing, catch the TypeInitializationException
                    doc.Save(outputPath);
                }
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                  "The PDF could not be saved using Aspose.Pdf's default renderer.");
                // Optionally, you could implement an alternative saving strategy here.
            }
        }
    }

    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
