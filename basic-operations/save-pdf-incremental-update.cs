using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        // Verify source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create a copy that will receive the incremental updates.
        File.Copy(inputPath, outputPath, overwrite: true);

        // Open the copy with read/write access via a FileStream.
        // Document must be constructed from a writable stream to enable incremental saving.
        using (FileStream stream = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(stream))
        {
            // Append a new blank page at the end of the document.
            Page newPage = doc.Pages.Add();

            // Add a simple text fragment to the new page (optional).
            TextFragment tf = new TextFragment("Appended page");
            tf.Position = new Position(100, 700); // Position within the page.
            newPage.Paragraphs.Add(tf);

            // Save the document using the parameterless Save() method.
            // This writes the modifications as an incremental update,
            // preserving the original content and structure.
            doc.Save();
        }

        Console.WriteLine($"Incremental update saved to '{outputPath}'.");
    }
}