using System;
using System.IO;
using System.Drawing; // required for Color in PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string url = "https://docs.aspose.com/pdf/net/";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create, load, save)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Facade editor
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Create a bookmark named "Help" that opens the specified URL
                // Parameters: title, color (System.Drawing.Color), bold, italic, file (null for URI), action type, destination URL
                editor.CreateBookmarksAction(
                    title: "Help",
                    color: System.Drawing.Color.Black,
                    boldFlag: true,
                    italicFlag: false,
                    file: null,
                    actionType: "URI",
                    destination: url);

                // Save the modified PDF (lifecycle: save)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
