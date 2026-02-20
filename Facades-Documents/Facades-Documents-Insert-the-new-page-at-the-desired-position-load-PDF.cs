using System;
using System.IO;
using Aspose.Pdf;

class InsertPageExample
{
    static void Main(string[] args)
    {
        // Input PDF path, output PDF path and the position where the new page will be inserted (1‑based)
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";
        int insertPosition = 2; // insert after the first page (1‑based index)

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        // Create a temporary blank PDF with a single empty page
        string blankPdf = Path.Combine(Path.GetTempPath(), "blank_page.pdf");
        try
        {
            // Create a new empty document and add one page
            Document blankDoc = new Document();
            blankDoc.Pages.Add(); // adds an empty page
            // Save the blank document to a temporary file (required for later insertion)
            blankDoc.Save(blankPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create blank page PDF: {ex.Message}");
            return;
        }

        try
        {
            // Load the source PDF
            Document srcDoc = new Document(inputPdf);
            // Load the blank page PDF (contains exactly one page)
            Document blankDoc = new Document(blankPdf);

            // Ensure the insert position is within the valid range
            if (insertPosition < 1 || insertPosition > srcDoc.Pages.Count + 1)
            {
                Console.Error.WriteLine($"Error: Insert position {insertPosition} is out of range.");
                return;
            }

            // Insert the blank page at the desired position
            // Pages.Insert uses a 1‑based index; the page to insert is taken from blankDoc.Pages[1]
            srcDoc.Pages.Insert(insertPosition, blankDoc.Pages[1]);

            // Save the modified PDF
            srcDoc.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page insertion: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary blank PDF file
            if (File.Exists(blankPdf))
            {
                try { File.Delete(blankPdf); } catch { /* ignore cleanup errors */ }
            }
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPdf}'.");
    }
}
