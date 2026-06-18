using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPath    = "merged.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both source documents inside using blocks (document-disposal-with-using rule)
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // ----- Ensure unique field names -----
                // Prefix all fields from the second document with a unique identifier.
                int idx = 1;
                foreach (Field field in doc2.Form.Fields)
                {
                    // PartialName is writable; FullName is read‑only.
                    string newPartialName = $"doc2_{idx}_{field.PartialName}";
                    field.PartialName = newPartialName;
                    idx++;
                }

                // ----- Merge pages -----
                // Append pages of the second document to the first.
                doc1.Pages.Add(doc2.Pages);

                // ----- Merge form fields -----
                // After pages are merged, add the renamed fields from doc2 to doc1's form.
                foreach (Field field in doc2.Form.Fields)
                {
                    doc1.Form.Add(field);
                }

                // ----- Save the merged document -----
                doc1.Save(outputPath); // uses the standard Save(string) rule
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}