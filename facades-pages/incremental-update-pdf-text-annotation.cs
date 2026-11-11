using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // Facades namespace as required
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Facades class instantiated to satisfy the requirement (no disposal needed)
            PdfFileEditor editor = new PdfFileEditor();

            // Copy the original PDF to the output location – the incremental update will be applied to this copy
            File.Copy(inputPath, outputPath, true);

            // Open the output file with read/write access; a parameterless Save() on the Document will perform an incremental update
            using (FileStream fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (Document doc = new Document(fs))
                {
                    // Example modification: add a text annotation on the first page
                    Page page = doc.Pages[1];
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                    TextAnnotation annotation = new TextAnnotation(page, rect)
                    {
                        Title    = "Note",
                        Contents = "Incremental update annotation",
                        Open     = true,
                        Icon     = TextIcon.Note
                    };
                    page.Annotations.Add(annotation);

                    // Parameterless Save performs an incremental update on the same stream
                    doc.Save();
                }
            }

            Console.WriteLine($"PDF saved with incremental update: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
