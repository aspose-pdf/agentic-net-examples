using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
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
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Ensure duplicate AcroForm field names are renamed to keep them unique
                // Aspose.Pdf no longer exposes Form.RenameDuplicateFields, so we rename manually.
                if (doc2.Form != null && doc2.Form.Fields != null && doc2.Form.Fields.Count() > 0)
                {
                    foreach (Field field in doc2.Form.Fields)
                    {
                        // If the first document already contains a field with the same name, rename it.
                        bool duplicate = doc1.Form != null &&
                                         doc1.Form.Fields != null &&
                                         doc1.Form.Fields.Any(f => f.PartialName == field.PartialName);
                        if (duplicate)
                        {
                            // Append a GUID suffix to make the name unique.
                            field.PartialName = $"{field.PartialName}_{Guid.NewGuid():N}";
                        }
                    }
                }

                // Append all pages from the second document to the first one
                foreach (Page page in doc2.Pages)
                {
                    // The Add method clones the page into the target document
                    doc1.Pages.Add(page);
                }

                // Save the merged result
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
