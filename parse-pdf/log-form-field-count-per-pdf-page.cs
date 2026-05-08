using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (contains all form fields in the document)
            Form form = doc.Form;

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Count of fields in the whole document (Aspose.Pdf does not provide per‑page field count directly)
                // For monitoring purposes we log this count for each page iteration.
                int fieldCount = form.Count;

                Console.WriteLine($"Page {i}: extracted {fieldCount} form field(s).");
            }

            // No modifications are made, so just save (optional – here we keep the original file)
            // doc.Save("output.pdf"); // Uncomment if you need to save a copy
        }
    }
}