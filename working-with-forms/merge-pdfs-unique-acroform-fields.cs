using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdf1Path = "first.pdf";
        const string pdf2Path = "second.pdf";
        const string outputPath = "merged_unique_fields.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        try
        {
            // Load the first PDF (target) – it will receive pages from the second PDF.
            using (Document targetDoc = new Document(pdf1Path))
            // Load the second PDF (source) – its fields will be renamed before merging.
            using (Document sourceDoc = new Document(pdf2Path))
            {
                // Prefix to make field names unique.
                const string prefix = "Doc2_";

                // Rename every form field in the source document.
                foreach (Field field in sourceDoc.Form)
                {
                    // The FullName property is read‑only; rename via the PartialName property.
                    field.PartialName = prefix + field.PartialName;
                }

                // Merge pages (including the renamed form fields) into the target document.
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Save the merged PDF.
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
