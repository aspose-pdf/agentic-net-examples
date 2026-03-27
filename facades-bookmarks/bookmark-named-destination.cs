using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and define a named destination on page 2
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document must have at least 2 pages.");
                return;
            }

            // Create a named destination called "MySection" in the document
            NamedDestination namedDest = new NamedDestination(doc, "MySection");

            // Add an invisible link annotation on page 2 that points to the named destination
            Page targetPage = doc.Pages[2];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            LinkAnnotation link = new LinkAnnotation(targetPage, rect);
            link.Action = new GoToAction(doc, "MySection");
            targetPage.Annotations.Add(link);

            // Save to a temporary file for the facade editor
            doc.Save(tempPath);
        }

        // Create a bookmark that points to the named destination
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPath);
        editor.CreateBookmarksAction(
            "Bookmark to MySection",
            System.Drawing.Color.Blue, // Fixed: use System.Drawing.Color
            false,
            false,
            null,
            "GoTo",
            "MySection");
        editor.Save(outputPath);
        editor.Close();

        // Remove the temporary file
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }

        Console.WriteLine($"Bookmark created and saved to '{outputPath}'.");
    }
}
