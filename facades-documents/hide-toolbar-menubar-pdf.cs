using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for intermediate and final PDF files
        const string tempPdfPath = "temp.pdf";
        const string outputPdfPath = "ToolbarMenubarHidden.pdf";

        // -----------------------------------------------------------------
        // 1. Create a new PDF document, add a page, and write simple text.
        //    Document implements IDisposable, so wrap it in a using block.
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Optional: add a text fragment so the page is not empty
            TextFragment tf = new TextFragment("Sample page with hidden toolbar and menubar.");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Save the document to a temporary file that will be processed
            // by PdfContentEditor.
            doc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Use PdfContentEditor (a Facades class) to modify viewer preferences.
        //    PdfContentEditor does NOT implement IDisposable, so no using block.
        // -----------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPdfPath);

        // Hide the toolbar and the menubar in PDF viewers.
        // ChangeViewerPreference accepts a single ViewerPreference argument;
        // combine the required flags with a bitwise OR.
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar | ViewerPreference.HideMenubar);

        // Save the modified PDF to the final output file.
        editor.Save(outputPdfPath);

        // -----------------------------------------------------------------
        // 3. Clean up the temporary file.
        // -----------------------------------------------------------------
        try
        {
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"PDF created with hidden toolbar and menubar: {outputPdfPath}");
    }
}
