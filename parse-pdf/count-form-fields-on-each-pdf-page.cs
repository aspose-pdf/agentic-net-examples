using System;
using System.Collections.Generic;
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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Dictionary to hold field count per page (1‑based page numbers)
                var fieldsPerPage = new Dictionary<int, int>();

                // Initialise counts to zero for all pages
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    fieldsPerPage[i] = 0;
                }

                // Iterate over form fields using the correct Aspose.Pdf.Forms.Field type
                if (doc.Form?.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Field.PageIndex is zero‑based; convert to 1‑based for logging
                        int pageNumber = field.PageIndex + 1;

                        // Ensure the page number is within the document range
                        if (pageNumber >= 1 && pageNumber <= doc.Pages.Count)
                        {
                            fieldsPerPage[pageNumber]++;
                        }
                    }
                }

                // Log the number of fields extracted from each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Console.WriteLine($"Page {i}: {fieldsPerPage[i]} form field(s) extracted.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
