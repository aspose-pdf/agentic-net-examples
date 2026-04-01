using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source and target PDFs exist – create simple placeholders if they don't.
        if (!File.Exists(targetPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(targetPath);
            }
        }

        if (!File.Exists(sourcePath))
        {
            using (var doc = new Document())
            {
                // Add a page with a distinct size and rotation to demonstrate preservation.
                var page = doc.Pages.Add();
                page.PageInfo.Width = 400;
                page.PageInfo.Height = 600;
                // Rotation enum values use the "on" prefix.
                page.Rotate = Rotation.on90;
                doc.Save(sourcePath);
            }
        }

        // Load the target PDF where the page will be inserted
        using (Document targetDoc = new Document(targetPath))
        {
            // Load the external PDF that contains the page to be inserted
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Insert the page from sourceDoc at position 2 (index is 1‑based) in the target document.
                // The Insert method copies the page, preserving its original size and rotation.
                targetDoc.Pages.Insert(2, sourceDoc.Pages[1]);
            }

            // Save the modified document
            targetDoc.Save(outputPath);
            Console.WriteLine($"Page inserted successfully. Saved as {outputPath}");
        }
    }
}
