using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "fields_bottom_half.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Prepare a writer for the output text file
            using (StreamWriter writer = new StreamWriter(outputTxt, false))
            {
                // Iterate over all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Determine page dimensions
                    double pageWidth = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;

                    // Define the bottom‑half rectangle (origin is bottom‑left)
                    Rectangle bottomHalf = new Rectangle(
                        llx: 0,
                        lly: 0,
                        urx: pageWidth,
                        ury: pageHeight / 2);

                    // Iterate over all form fields in the document
                    foreach (Field field in doc.Form.Fields)
                    {
                        // The Field class exposes the zero‑based PageIndex property.
                        // Convert it to 1‑based to compare with the current pageIndex.
                        if (field.PageIndex + 1 != pageIndex)
                            continue;

                        // Check if the field lies completely within the bottom half
                        Rectangle fieldRect = field.Rect;
                        bool insideBottomHalf = fieldRect.LLY >= bottomHalf.LLY &&
                                                fieldRect.URY <= bottomHalf.URY;

                        if (insideBottomHalf)
                        {
                            // Retrieve the field's value as a string (handles different field types)
                            string value = field.Value?.ToString() ?? string.Empty;
                            // Write a line: PageNumber, FieldName, Value
                            writer.WriteLine($"Page {pageIndex}, Field \"{field.FullName}\": {value}");
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Field values from bottom half saved to '{outputTxt}'.");
    }
}
