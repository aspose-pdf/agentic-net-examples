using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path to the original PDF file
        const string inputPath = "original.pdf";

        // Path for the new PDF file that will preserve the original
        const string outputPath = "modified_copy.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF, make any desired modifications, and save to a new file
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a simple text annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Modified PDF copy",
                Open     = true,
                Icon     = TextIcon.Note,
                Color    = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(annotation);

            // Save the modified document to a new file name
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'. Original remains unchanged.");
    }
}