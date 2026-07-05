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
        const string outputPdfPath = "merged.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        try
        {
            // Load the first document (target) and the second document (source) using using blocks.
            using (Document target = new Document(firstPdfPath))
            using (Document source = new Document(secondPdfPath))
            {
                // Rename all form fields in the source document to avoid name collisions.
                // Prefix with "Src_" (or any unique identifier).
                foreach (Field field in source.Form)
                {
                    // Field.Name is writable; prepend a unique prefix.
                    field.Name = "Src_" + field.Name;
                }

                // Merge pages (and associated form fields) from source into target.
                target.Pages.Add(source.Pages);

                // Save the merged document.
                target.Save(outputPdfPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}