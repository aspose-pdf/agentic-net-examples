using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_js.pdf";

        // Load existing PDF or create a new one if the file does not exist
        Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document();

        // Ensure there is at least one page to work with
        if (doc.Pages.Count == 0)
        {
            doc.Pages.Add();
        }

        // Wrap the document in a using block for deterministic disposal
        using (doc)
        {
            // -----------------------------------------------------------------
            // 1. Add a document‑level JavaScript action (executed after saving)
            // -----------------------------------------------------------------
            JavascriptAction afterSaveJs = new JavascriptAction("app.alert('Document saved!');");
            doc.Actions.AfterSaving = afterSaveJs;

            // ---------------------------------------------------------------
            // 2. Add a link annotation that runs JavaScript when clicked
            // ---------------------------------------------------------------
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            // Define the clickable rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            LinkAnnotation jsLink = new LinkAnnotation(firstPage, linkRect);
            // Assign a JavaScript action to the link
            jsLink.Action = new JavascriptAction("app.alert('Link clicked!');");
            // Add the annotation to the page
            firstPage.Annotations.Add(jsLink);

            // ---------------------------------------------------------------
            // 3. Save the PDF with the embedded JavaScript actions
            // ---------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript actions saved to '{outputPath}'.");
    }
}