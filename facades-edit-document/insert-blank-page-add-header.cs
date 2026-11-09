using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // original PDF
        const string outputPath = "output.pdf";    // result PDF with blank page and header
        const string blankPath = "blank.pdf";      // temporary one‑page blank PDF

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Create a temporary PDF that contains a single blank page.
        // -----------------------------------------------------------------
        using (Document blankDoc = new Document())
        {
            // Add an empty page (Aspose.Pdf uses 1‑based indexing)
            blankDoc.Pages.Add();
            // Save the temporary blank PDF
            blankDoc.Save(blankPath);
        }

        // -----------------------------------------------------------------
        // 2. Prepare the output file (copy original so we keep the source intact)
        // -----------------------------------------------------------------
        if (File.Exists(outputPath))
            File.Delete(outputPath);
        File.Copy(inputPath, outputPath);

        // -----------------------------------------------------------------
        // 3. Insert the blank page at position 3 using Document API (PdfFileEditor.InsertPage does not exist)
        // -----------------------------------------------------------------
        // Load the destination PDF (the copy we just created)
        Document destDoc = new Document(outputPath);
        // Load the temporary blank PDF that contains the page we want to insert
        Document srcDoc = new Document(blankPath);
        // Insert the first (and only) page of srcDoc at position 3 of destDoc
        // Document.Pages.Insert uses 1‑based indexing; the new page becomes page 3
        destDoc.Pages.Insert(3, srcDoc.Pages[1]);
        // Save the document after insertion
        destDoc.Save(outputPath);

        // -----------------------------------------------------------------
        // 4. Add a header annotation on the newly inserted page (page 3)
        // -----------------------------------------------------------------
        Document doc = new Document(outputPath);
        // Ensure the document now has at least 3 pages
        if (doc.Pages.Count < 3)
        {
            Console.Error.WriteLine("The document does not contain a third page after insertion.");
            return;
        }
        Page page = doc.Pages[3];
        // Define a rectangle for the annotation (coordinates are in points, origin at bottom‑left)
        // Here we place it near the top of an A4 page (595x842 points)
        Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(0, 800, 595, 842);
        // Create the text annotation
        TextAnnotation txtAnn = new TextAnnotation(page, headerRect)
        {
            Title = "Header",
            Contents = "Inserted blank page header"
        };
        page.Annotations.Add(txtAnn);
        // Save the changes back to the same file
        doc.Save(outputPath);

        // -----------------------------------------------------------------
        // 5. Clean up temporary blank PDF
        // -----------------------------------------------------------------
        if (File.Exists(blankPath))
            File.Delete(blankPath);

        Console.WriteLine($"Blank page inserted at position 3 and header annotation added. Output saved to '{outputPath}'.");
    }
}
