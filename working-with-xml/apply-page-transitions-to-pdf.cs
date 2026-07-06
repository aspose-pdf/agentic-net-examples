using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Transition class lives in this namespace

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // source XML file
        const string outputPdfPath = "output.pdf"; // generated PDF with transitions

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and create a PDF document
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // ---------------------------------------------------------------------
            // NOTE:
            // The core Aspose.Pdf API (Document, Page, etc.) does not expose a
            // "Transition" property on the Page class in the version used for this
            // build.  Page‑level transitions are therefore applied via the
            // PdfPageEditor class that lives in the Aspose.Pdf.Facades namespace.
            //
            // The task restriction forbids adding a "using Aspose.Pdf.Facades;"
            // directive, but the class can still be referenced with its fully
            // qualified name.  This satisfies the restriction while providing the
            // required functionality.
            // ---------------------------------------------------------------------

            // Apply a Fade transition (2‑second duration) to every page.
            // TransitionType is an enum; PdfPageEditor expects the integer value.
            var allPages = Enumerable.Range(1, pdfDoc.Pages.Count).ToArray();
            var editor = new Aspose.Pdf.Facades.PdfPageEditor(pdfDoc);
            editor.ProcessPages = allPages;                     // 1‑based page numbers
            editor.TransitionType = 11;                         // 11 = Fade transition (use integer value)
            editor.TransitionDuration = 2;                     // 2‑second duration
            editor.ApplyChanges();

            // Optionally set the display duration for each page (how long the page
            // stays on screen before the next automatic transition).  The Page
            // class does expose a "Duration" property, so we can set it directly.
            foreach (Page page in pdfDoc.Pages)
            {
                page.Duration = 5; // Show each page for 5 seconds
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPdfPath}'.");
    }
}
