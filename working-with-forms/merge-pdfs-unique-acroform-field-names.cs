using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load source documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // -----------------------------------------------------------------
                // 1. Ensure duplicate AcroForm field names are unique before merging.
                // -----------------------------------------------------------------
                // Collect all field names that already exist in the first document.
                var existingNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Field f in doc1.Form.Fields)
                {
                    if (!string.IsNullOrEmpty(f.PartialName))
                        existingNames.Add(f.PartialName);
                }

                // Rename fields in the second document that clash with the first document.
                foreach (Field f in doc2.Form.Fields)
                {
                    if (string.IsNullOrEmpty(f.PartialName))
                        continue;

                    if (existingNames.Contains(f.PartialName))
                    {
                        // Create a new unique name – you can use any scheme you prefer.
                        string newName;
                        int counter = 1;
                        do
                        {
                            newName = $"{f.PartialName}_dup{counter}";
                            counter++;
                        } while (existingNames.Contains(newName));

                        f.PartialName = newName; // rename the field
                    }

                    // Add the (original or renamed) name to the set so subsequent fields are also checked.
                    existingNames.Add(f.PartialName);
                }

                // ---------------------------------------------------------------
                // 2. Append pages of the second document to the first document.
                // ---------------------------------------------------------------
                // The Pages.Add overload that accepts a PageCollection copies all pages.
                doc1.Pages.Add(doc2.Pages);

                // Save the merged PDF
                doc1.Save(outputPdf);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}
