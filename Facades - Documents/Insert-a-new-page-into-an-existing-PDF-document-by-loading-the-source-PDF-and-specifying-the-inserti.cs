using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string outputPdf = "output.pdf";
        const string blankPdf = "blank.pdf";
        const int insertAt = 2; // position where the new page will be inserted

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Create a temporary PDF containing a single blank page
        using (Document blankDoc = new Document())
        {
            blankDoc.Pages.Insert(1); // insert an empty page at the beginning
            blankDoc.Save(blankPdf);
        }

        // Insert the blank page into the source PDF at the specified location
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Insert(sourcePdf, insertAt, blankPdf, 1, 1, outputPdf);

        if (result)
            Console.WriteLine($"Page inserted successfully. Output saved to '{outputPdf}'.");
        else
            Console.Error.WriteLine("Failed to insert page.");

        // Clean up the temporary blank PDF
        try { File.Delete(blankPdf); } catch { }
    }
}