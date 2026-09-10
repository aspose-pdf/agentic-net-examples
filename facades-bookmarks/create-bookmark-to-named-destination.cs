using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string destinationName = "MyNamedDestination";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a bookmark that points to a named destination.
            // The named destination must exist elsewhere in the PDF (e.g., defined by a link annotation).
            // Here we create the bookmark using PdfContentEditor.CreateBookmarksAction.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Create the bookmark:
            // title: "Go to MyNamedDestination"
            // color: Blue (System.Drawing.Color to avoid ambiguity)
            // boldFlag: true, italicFlag: false
            // file: null (not needed for GoTo action)
            // actionType: "GoTo"
            // destination: the name of the destination defined elsewhere
            editor.CreateBookmarksAction(
                "Go to MyNamedDestination",
                System.Drawing.Color.Blue,
                true,
                false,
                null,
                "GoTo",
                destinationName);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark created and saved to '{outputPath}'.");
    }
}