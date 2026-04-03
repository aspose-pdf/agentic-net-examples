using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        try
        {
            // 1. Merge PDFs using PdfFileEditor.Concatenate
            PdfFileEditor editor = new PdfFileEditor();
            string[] sourceFiles = { pdf1Path, pdf2Path };
            editor.Concatenate(sourceFiles, outputPath);

            // 2. Load the merged document and rename duplicate AcroForm field names
            using (Document mergedDoc = new Document(outputPath))
            {
                RenameDuplicateFields(mergedDoc);
                mergedDoc.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void RenameDuplicateFields(Document doc)
    {
        // Tracks how many times each field name has been seen.
        var nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var field in doc.Form.Fields)
        {
            // All form field types inherit from WidgetAnnotation and expose PartialName.
            string originalName = field.PartialName;
            if (string.IsNullOrEmpty(originalName))
                continue;

            if (nameCounts.TryGetValue(originalName, out int count))
            {
                // Duplicate found – generate a new unique name.
                count++;
                nameCounts[originalName] = count;
                string newName = $"{originalName}_{count}";
                field.PartialName = newName;
            }
            else
            {
                // First occurrence of this name.
                nameCounts[originalName] = 0;
            }
        }
    }
}