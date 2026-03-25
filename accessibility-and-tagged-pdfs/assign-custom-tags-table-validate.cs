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
        const string outputPdf = "tagged_table.pdf";
        const string validationLog = "validation.log";

        // ---------------------------------------------------------------------
        // 1. Create PDF with a tagged table and custom cell tags (AlternativeText)
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Prepare tagged‑content infrastructure
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Custom Cell Tags");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with cell data‑type tags";
            root.AppendChild(table);

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");
            thName.AlternativeText = "String"; // custom tag
            headerRow.AppendChild(thName);

            TableTHElement thAge = tagged.CreateTableTHElement();
            thAge.SetText("Age");
            thAge.AlternativeText = "Integer"; // custom tag
            headerRow.AppendChild(thAge);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);

            TableTDElement tdAlice = tagged.CreateTableTDElement();
            tdAlice.SetText("Alice");
            tdAlice.AlternativeText = "String"; // custom tag
            row1.AppendChild(tdAlice);

            TableTDElement td30 = tagged.CreateTableTDElement();
            td30.SetText("30");
            td30.AlternativeText = "Integer"; // custom tag
            row1.AppendChild(td30);

            // Second data row
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);

            TableTDElement tdBob = tagged.CreateTableTDElement();
            tdBob.SetText("Bob");
            tdBob.AlternativeText = "String"; // custom tag
            row2.AppendChild(tdBob);

            TableTDElement td45 = tagged.CreateTableTDElement();
            td45.SetText("45");
            td45.AlternativeText = "Integer"; // custom tag
            row2.AppendChild(td45);

            // ---------------------------------------------------------------
            // 2. Save the PDF – Aspose.Pdf works on all platforms; guard only
            //    against a possible DllNotFoundException (e.g., missing libgdiplus).
            // ---------------------------------------------------------------
            try
            {
                doc.Save(outputPdf);
                Console.WriteLine($"PDF saved to '{outputPdf}'.");
            }
            catch (DllNotFoundException ex)
            {
                Console.WriteLine($"Warning: Required native library not found – PDF was not saved. Details: {ex.Message}");
                // Exit early – there is nothing to validate.
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving PDF: {ex.Message}");
                return;
            }
        }

        // ---------------------------------------------------------------
        // 3. Validate the generated PDF (PDF/A‑1B compliance). If the file
        //    could not be created the previous block already returned.
        // ---------------------------------------------------------------
        if (File.Exists(outputPdf))
        {
            using (Document docToValidate = new Document(outputPdf))
            {
                bool isValid = false;
                try
                {
                    isValid = docToValidate.Validate(validationLog, PdfFormat.PDF_A_1B);
                }
                catch (DllNotFoundException ex)
                {
                    Console.WriteLine($"Warning: Required native library not found – validation skipped. Details: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Validation failed with unexpected error: {ex.Message}");
                }

                Console.WriteLine($"Validation result: {(isValid ? "Valid" : "Invalid")}");
                Console.WriteLine($"Log written to {validationLog}");
            }
        }
        else
        {
            Console.WriteLine("PDF file not found – validation skipped.");
        }
    }
}
