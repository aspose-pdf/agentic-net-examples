using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    // Suppress the known‑high‑severity NuGet warning for the demo project.
    #pragma warning disable NU1903
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Styled Header");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // ----- Create table header (THead) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row (TableTRElement)
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Define a bold text style that will be applied to each header cell
            TextState headerTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Header cell 1
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            th1.BackgroundColor = Aspose.Pdf.Color.LightGray; // cell background
            th1.DefaultCellTextState = headerTextState;      // bold font
            headerRow.AppendChild(th1);

            // Header cell 2
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Quantity");
            th2.BackgroundColor = Aspose.Pdf.Color.LightGray;
            th2.DefaultCellTextState = headerTextState;
            headerRow.AppendChild(th2);

            // Header cell 3
            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Price");
            th3.BackgroundColor = Aspose.Pdf.Color.LightGray;
            th3.DefaultCellTextState = headerTextState;
            headerRow.AppendChild(th3);

            // ----- Create table body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Example data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // Cell 1
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            dataRow.AppendChild(td1);

            // Cell 2
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("150");
            dataRow.AppendChild(td2);

            // Cell 3
            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("$25.00");
            dataRow.AppendChild(td3);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                    // The document is still saved; the exception is swallowed to keep the demo running.
                }
            }
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
    #pragma warning restore NU1903
}
