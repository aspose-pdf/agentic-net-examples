using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_modified.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // STEP 1: Extract all text from the PDF using PdfExtractor
        // ------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);               // Load the PDF
        extractor.ExtractText();                       // Perform text extraction

        // Prepare a list that will hold the extracted list items.
        List<string> listItems = new List<string>();

        // Retrieve extracted text into a memory stream
        using (MemoryStream textStream = new MemoryStream())
        {
            extractor.GetText(textStream);
            string fullText = Encoding.UTF8.GetString(textStream.ToArray());

            // ------------------------------------------------------------
            // STEP 2: Parse list items from the extracted text
            // ------------------------------------------------------------
            // Define a simple pattern for list items:
            //   - Bulleted items starting with '-', '*', or '•'
            //   - Numbered items like "1.", "2)", etc.
            Regex listItemPattern = new Regex(@"^\s*([\-\*\u2022]|\d+[\.\)])\s+.+", RegexOptions.Multiline);
            MatchCollection matches = listItemPattern.Matches(fullText);

            foreach (Match match in matches)
            {
                listItems.Add(match.Value.Trim());
            }

            // Display extracted list items
            Console.WriteLine("Extracted list items:");
            foreach (string item in listItems)
            {
                Console.WriteLine($"- {item}");
            }

            // ------------------------------------------------------------
            // STEP 3: Modify a specific list entry (if any)
            // ------------------------------------------------------------
            // Example: replace the first occurrence of a specific old item with a new one.
            // Adjust these strings as needed for your scenario.
            string oldItem = "Old List Entry";
            string newItem = "New List Entry";

            // Find the first list item that contains the old text
            string targetItem = null;
            foreach (string item in listItems)
            {
                if (item.IndexOf(oldItem, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    targetItem = item;
                    break;
                }
            }

            if (targetItem != null)
            {
                // Use PdfContentEditor to replace the text on the first page.
                // ReplaceText(string oldText, int pageNumber, string newText) limits the operation to a single page.
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(inputPdfPath);
                editor.ReplaceText(oldItem, 1, newItem); // page number is 1‑based
                editor.Save(outputPdfPath);
                Console.WriteLine($"Modified PDF saved as '{outputPdfPath}'.");
            }
            else
            {
                Console.WriteLine($"No list item containing \"{oldItem}\" was found. No modification performed.");
            }
        }

        // ------------------------------------------------------------
        // STEP 4: (Optional) Extract a specific entry to a separate text file
        // ------------------------------------------------------------
        // For demonstration, write the first extracted list item to a file.
        if (listItems.Count > 0)
        {
            string firstItemPath = "first_list_item.txt";
            File.WriteAllText(firstItemPath, listItems[0], Encoding.UTF8);
            Console.WriteLine($"First list item written to '{firstItemPath}'.");
        }
    }
}
