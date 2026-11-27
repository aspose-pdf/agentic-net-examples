using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // <-- added for Field and concrete form types
using Aspose.Pdf.Annotations; // kept for completeness (not used here)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputCsvPath = "fields_top_left_quarter.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Assume we work with the first page; adjust if needed
            Page page = doc.Pages[1]; // 1‑based indexing (global rule)

            // Determine the bounds of the top‑left quarter rectangle
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Top‑left quarter: left = 0, bottom = pageHeight/2, right = pageWidth/2, top = pageHeight
            double quarterLLX = 0;
            double quarterLLY = pageHeight / 2.0;
            double quarterURX = pageWidth / 2.0;
            double quarterURY = pageHeight;

            // Collect field names that lie completely inside the quarter rectangle
            List<string> fieldNames = new List<string>();

            // Iterate over all form fields in the document
            foreach (Field field in doc.Form) // Field now resolves because of Aspose.Pdf.Forms namespace
            {
                // Each field has a Rect property (Aspose.Pdf.Rectangle) defining its position
                Aspose.Pdf.Rectangle rect = field.Rect;

                // Check if the field rectangle is fully inside the quarter rectangle
                bool insideQuarter =
                    rect.LLX >= quarterLLX &&
                    rect.LLY >= quarterLLY &&
                    rect.URX <= quarterURX &&
                    rect.URY <= quarterURY;

                if (insideQuarter)
                {
                    // Field.Name gives the field's name
                    fieldNames.Add(field.Name);
                }
            }

            // Write the collected names to a CSV file (manual CSV, not a PDF save option)
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                writer.WriteLine("FieldName"); // header
                foreach (string name in fieldNames)
                {
                    // Escape double quotes by doubling them (basic CSV safety)
                    string escaped = name.Replace("\"", "\"\"");
                    writer.WriteLine($"\"{escaped}\"");
                }
            }

            Console.WriteLine($"Saved {fieldNames.Count} field name(s) to '{outputCsvPath}'.");
        }
    }
}
