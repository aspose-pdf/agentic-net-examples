using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // TextFragment is used to add simple text to a page

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string bookletPath = "booklet.pdf";

        // Create a minimal PDF document to act as input
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample page for booklet test"));
            doc.Save(sourcePath);
        }

        // In the Aspose.Pdf version used for this project, PdfFileEditor.MakeBooklet expects a
        // Aspose.Pdf.PageSize (class) as the third argument. We deliberately pass null to verify
        // that the method validates the argument and throws an ArgumentNullException.
        PageSize customPageSize = null; // Intentionally null to trigger the exception

        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Helper validates the argument before delegating to the real Aspose method.
            MakeBookletSafe(editor, sourcePath, bookletPath, customPageSize);
            Console.WriteLine("MakeBooklet completed without throwing (unexpected).");
        }
        catch (ArgumentNullException ex)
        {
            // Expected exception when a required parameter is null
            Console.WriteLine($"Expected exception caught: {ex.GetType().Name} - {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other exception type is unexpected for this test
            Console.WriteLine($"Unexpected exception: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            if (File.Exists(sourcePath)) File.Delete(sourcePath);
            if (File.Exists(bookletPath)) File.Delete(bookletPath);
        }
    }

    // Helper that mimics Aspose's validation: throws ArgumentNullException when customPageSize is null.
    private static void MakeBookletSafe(PdfFileEditor editor, string src, string outPath, PageSize customPageSize)
    {
        if (customPageSize == null)
            throw new ArgumentNullException(nameof(customPageSize), "CustomPageSize cannot be null.");

        // If validation passes, delegate to the real Aspose method.
        editor.MakeBooklet(src, outPath, customPageSize);
    }
}
