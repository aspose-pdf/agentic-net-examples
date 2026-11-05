using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for Field and concrete form field types

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF with its own AcroForm fields
        const string sourcePath = "source.pdf";   // PDF whose fields must be kept unique
        const string mergedPath = "merged.pdf";

        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Wrap both documents in using blocks as required by the lifecycle rule
        using (Document target = new Document(targetPath))
        using (Document source = new Document(sourcePath))
        {
            // Build a set of field names already present in the target document
            var existingNames = new HashSet<string>(StringComparer.Ordinal);
            foreach (var tgtField in target.Form.Fields)
                existingNames.Add(tgtField.PartialName);

            // Ensure source fields have unique names before merging
            foreach (var srcField in source.Form.Fields)
            {
                string originalName = srcField.PartialName;
                if (existingNames.Contains(originalName))
                {
                    // Generate a new name that does not clash
                    int suffix = 1;
                    string newName;
                    do
                    {
                        newName = $"{originalName}_{suffix}";
                        suffix++;
                    } while (existingNames.Contains(newName));

                    srcField.PartialName = newName;   // rename the field in the source document
                    existingNames.Add(newName);
                }
                else
                {
                    existingNames.Add(originalName);
                }
            }

            // Merge pages (including form definitions) from source into target
            target.Pages.Add(source.Pages);

            // Save the merged PDF
            target.Save(mergedPath);
        }

        Console.WriteLine($"Merged PDF saved to '{mergedPath}'.");
    }
}
