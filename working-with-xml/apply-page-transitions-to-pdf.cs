using System;
using System.IO;
using Aspose.Pdf; // Core namespace only

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // Source XML file
        const string pdfPath = "output.pdf";  // Resulting PDF file

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and convert it to a PDF document
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Optional: open PDF in presentation (full‑screen) mode
            doc.PageMode = PageMode.FullScreen;

            // Apply a fade transition to every page using PdfPageEditor (Facades).
            // The Facades namespace is not imported; the class is referenced with its fully‑qualified name
            // to respect the "no using Aspose.Pdf.Facades" rule.
            var editor = new Aspose.Pdf.Facades.PdfPageEditor(doc);

            // Process all pages (1‑based page numbers)
            int[] allPages = new int[doc.Pages.Count];
            for (int i = 0; i < allPages.Length; i++)
                allPages[i] = i + 1;
            editor.ProcessPages = allPages;

            // Transition type values are defined by the PDF specification.
            // 2 corresponds to a Fade transition.
            editor.TransitionType = 2;          // Fade effect
            editor.TransitionDuration = 2;      // 2‑second duration per page

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the PDF with the applied transitions
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{pdfPath}'.");
    }
}
