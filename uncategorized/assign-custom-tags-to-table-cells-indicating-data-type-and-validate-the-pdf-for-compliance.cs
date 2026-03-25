using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "tagged_table.pdf";
        const string logPath = "validation_log.txt";

        using (Document doc = new Document())
        {
            // Add a blank page to host the content
            Page page = doc.Pages.Add();

            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Custom Cell Tags");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table structure element and attach it to the root
            TableElement tableStruct = tagged.CreateTableElement();
            tableStruct.AlternativeText = "Sample data table";
            root.AppendChild(tableStruct);

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            tableStruct.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            string[] headers = { "Name", "Age", "Salary" };
            foreach (var h in headers)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText(h);
                th.AlternativeText = "Header"; // custom tag for header cells
                headerRow.AppendChild(th);
            }

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            tableStruct.AppendChild(tbody);

            var data = new[]
            {
                new { Name = "Alice", Age = 30, Salary = 70000 },
                new { Name = "Bob",   Age = 45, Salary = 85000 }
            };

            foreach (var row in data)
            {
                TableTRElement bodyRow = tagged.CreateTableTRElement();
                tbody.AppendChild(bodyRow);

                // Name – string type
                TableTDElement tdName = tagged.CreateTableTDElement();
                tdName.SetText(row.Name);
                tdName.AlternativeText = "String";
                bodyRow.AppendChild(tdName);

                // Age – numeric type
                TableTDElement tdAge = tagged.CreateTableTDElement();
                tdAge.SetText(row.Age.ToString());
                tdAge.AlternativeText = "Number";
                bodyRow.AppendChild(tdAge);

                // Salary – currency type
                TableTDElement tdSalary = tagged.CreateTableTDElement();
                tdSalary.SetText(row.Salary.ToString("C"));
                tdSalary.AlternativeText = "Currency";
                bodyRow.AppendChild(tdSalary);
            }

            // ---------- Save the PDF (guarded for non‑Windows platforms) ----------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform; PDF not saved.");
                }
            }

            // ---------- Validate the document (guarded similarly) ----------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation result: {isValid}. Log written to {logPath}");
            }
            else
            {
                try
                {
                    bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                    Console.WriteLine($"Validation result: {isValid}. Log written to {logPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available; validation skipped.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
