using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "incremental_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Copy the original PDF to the output location – incremental update works on an existing file.
            File.Copy(inputPath, outputPath, true);

            // Open the copied file with read/write access.
            using (FileStream fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                // Load the document from the stream.
                using (Document doc = new Document(fs))
                {
                    // Add annotation to the first page.
                    Page page = doc.Pages[1];
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    TextAnnotation annotation = new TextAnnotation(page, rect)
                    {
                        Title = "Note",
                        Contents = "Incremental update example.",
                        Open = true,
                        Icon = TextIcon.Note
                    };

                    page.Annotations.Add(annotation);

                    // Parameterless Save performs an incremental update when the document was opened from a read/write stream.
                    doc.Save();
                }
            }

            Console.WriteLine($"Incremental update saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
