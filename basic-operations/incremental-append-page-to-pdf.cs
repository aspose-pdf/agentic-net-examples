using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        // Ensure the source PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(sourcePath))
        {
            using (var seed = new Document())
            {
                seed.Pages.Add();
                seed.Save(sourcePath);
            }
        }

        // Open the PDF with a writable stream. Incremental saving works only when the
        // document is backed by a stream that supports read/write access.
        using (FileStream stream = new FileStream(sourcePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
        using (Document doc = new Document(stream))
        {
            // Append a new blank page.
            Page newPage = doc.Pages.Add();

            // Add simple text to the new page.
            TextFragment tf = new TextFragment("This page was appended using incremental update.");
            newPage.Paragraphs.Add(tf);

            // Save incrementally – only the changes are written to the existing file.
            doc.Save();
        }

        // If a separate output file is required, copy the updated source file.
        if (!string.Equals(sourcePath, outputPath, StringComparison.OrdinalIgnoreCase))
        {
            File.Copy(sourcePath, outputPath, overwrite: true);
        }

        Console.WriteLine($"Incremental update completed. Output saved to '{outputPath}'.");
    }
}
