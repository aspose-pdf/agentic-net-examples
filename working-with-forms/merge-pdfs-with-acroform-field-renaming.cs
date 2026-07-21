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
        const string outputPath = "merged.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Rename all form fields in the second document to avoid name collisions
            foreach (Field field in doc2.Form.Fields)
            {
                string originalName = field.Name;
                field.Name = $"Doc2_{originalName}";
                // Also adjust PartialName to keep consistency
                field.PartialName = $"Doc2_{field.PartialName}";
            }

            // Merge the second document into the first one
            doc1.Merge(doc2);

            // Save the merged result
            doc1.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}