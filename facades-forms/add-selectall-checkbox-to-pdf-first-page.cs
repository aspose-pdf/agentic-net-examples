using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // CheckboxField

class Program
{
    static void Main()
    {
        // Collection of PDF file paths to process
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_SelectAll.pdf");

            // Open the PDF document (lifecycle rule: using block)
            using (Document doc = new Document(inputPath))
            {
                // First page (1‑based indexing)
                Page firstPage = doc.Pages[1];

                // Define the checkbox rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

                // Create the checkbox field on the first page
                CheckboxField checkbox = new CheckboxField(firstPage, rect);
                checkbox.Name = "SelectAll";   // field name
                checkbox.Checked = false;      // default unchecked

                // Add the field to the document's form
                doc.Form.Add(checkbox);

                // Save the modified PDF (PDF format, no extra SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed and saved: {outputPath}");
        }
    }
}