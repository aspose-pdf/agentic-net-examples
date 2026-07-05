using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";
        const string destinationName = "MyNamedDestination";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to define a named destination (e.g., page 2, fit to window)
        Document doc = new Document(inputPath);
        // Add the named destination to the document's NamedDestinations collection
        // FitExplicitDestination makes the view fit the whole page
        doc.NamedDestinations.Add(destinationName, new FitExplicitDestination(doc.Pages[2]));

        // Save to a temporary file so the facade can work with the updated document
        string tempPath = Path.GetTempFileName();
        doc.Save(tempPath);

        // Use PdfContentEditor to create a bookmark that points to the named destination
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPath);
        editor.CreateBookmarksAction(
            title: "Bookmark to Named Destination",   // bookmark title
            color: System.Drawing.Color.DarkGreen,    // title color – use System.Drawing.Color
            boldFlag: true,                          // bold style
            italicFlag: false,                       // italic style
            file: null,                              // not needed for GoTo action
            actionType: "GoTo",                    // action type that uses a named destination
            destination: destinationName);            // the name defined above
        editor.Save(outputPath);
        editor.Close();

        // Clean up the temporary file
        File.Delete(tempPath);

        Console.WriteLine($"Bookmark created and saved to '{outputPath}'.");
    }
}
