using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";   // PDF that will receive the page
        const string sourcePath = "source.pdf";   // PDF containing the page to insert
        const string outputPath = "merged.pdf";   // Resulting PDF
        const int insertPosition = 2;              // 1‑based position where the page will be inserted

        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document targetDoc = new Document(targetPath))
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Retrieve the page to be inserted (first page of source PDF)
                Page pageToInsert = sourceDoc.Pages[1];

                // Insert the page into the target document at the specified position
                targetDoc.Pages.Insert(insertPosition, pageToInsert);

                // Save the modified document
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}