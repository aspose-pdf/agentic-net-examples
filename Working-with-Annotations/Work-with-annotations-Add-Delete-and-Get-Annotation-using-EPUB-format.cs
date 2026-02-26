using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // -------------------------------------------------
            // Add a TextAnnotation to the first page
            // -------------------------------------------------
            Aspose.Pdf.Page page = doc.Pages[1]; // 1‑based indexing

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the annotation
            Aspose.Pdf.Annotations.TextAnnotation textAnno = new Aspose.Pdf.Annotations.TextAnnotation(page, rect);
            textAnno.Title   = "MyNote";               // title shown in the popup title bar
            textAnno.Subject = "Sample Subject";       // optional description
            textAnno.Contents = "This is a sample text annotation."; // popup contents
            textAnno.Open    = true;                   // open the popup by default

            // Add the annotation to the page
            page.Annotations.Add(textAnno);

            // -------------------------------------------------
            // Get (list) all markup annotations on the first page
            // -------------------------------------------------
            Console.WriteLine("Annotations on page 1:");
            foreach (Aspose.Pdf.Annotations.Annotation anno in page.Annotations)
            {
                // Only markup annotations have Title/Subject/Contents
                if (anno is Aspose.Pdf.Annotations.MarkupAnnotation markup)
                {
                    Console.WriteLine($"- Title:   {markup.Title}");
                    Console.WriteLine($"  Subject: {markup.Subject}");
                    Console.WriteLine($"  Contents:{markup.Contents}");
                }
            }

            // -------------------------------------------------
            // Delete the annotation we just added (identified by Title)
            // -------------------------------------------------
            for (int i = page.Annotations.Count; i >= 1; i--) // iterate backwards when removing
            {
                Aspose.Pdf.Annotations.Annotation anno = page.Annotations[i];
                if (anno is Aspose.Pdf.Annotations.MarkupAnnotation markup && markup.Title == "MyNote")
                {
                    page.Annotations.Delete(i);
                    Console.WriteLine("Deleted annotation with Title='MyNote'.");
                }
            }

            // -------------------------------------------------
            // Save the modified document as EPUB
            // -------------------------------------------------
            Aspose.Pdf.EpubSaveOptions epubOptions = new Aspose.Pdf.EpubSaveOptions();
            doc.Save(outputEpubPath, epubOptions);
            Console.WriteLine($"Document saved as EPUB to '{outputEpubPath}'.");
        }
    }
}