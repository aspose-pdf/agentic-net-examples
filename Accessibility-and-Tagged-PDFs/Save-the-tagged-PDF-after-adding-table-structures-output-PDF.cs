using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Access the tagged‑content interface
                ITaggedContent tagged = pdfDoc.TaggedContent;

                // Optional: set document language and title for accessibility
                tagged.SetLanguage("en-US");
                tagged.SetTitle("PDF with Tagged Table");

                // ------------------------------------------------------------
                // Build a simple logical table structure
                // ------------------------------------------------------------

                // Create the root Table element
                var table = tagged.CreateTableElement();

                // Create a Table Body (TBody) and attach it to the table
                var tbody = tagged.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // Create a single Table Row (TR) and attach it to the body
                var row = tagged.CreateTableTRElement();
                tbody.AppendChild(row);

                // Create a single Table Cell (TD) and attach it to the row
                var cell = tagged.CreateTableTDElement();
                row.AppendChild(cell);

                // Inside the cell, create a Paragraph element
                var paragraph = tagged.CreateParagraphElement();
                cell.AppendChild(paragraph);

                // ------------------------------------------------------------
                // Add visible content that corresponds to the logical structure
                // ------------------------------------------------------------
                // Here we simply add a TextFragment to the first page.
                // In a real scenario you would position the text appropriately.
                TextFragment tf = new TextFragment("Sample cell content");
                pdfDoc.Pages[1].Paragraphs.Add(tf);

                // Prepare the tagged content for saving
                tagged.PreSave();

                // Persist the logical structure into the PDF document
                tagged.Save();

                // Save the modified PDF to the output file (uses the provided save rule)
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}