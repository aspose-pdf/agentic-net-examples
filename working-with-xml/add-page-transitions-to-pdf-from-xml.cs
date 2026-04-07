using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfPageEditor

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Verify the source XML exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML and generate a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions(); // Correct load options for XML
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Set the presentation duration for each page
            foreach (Page page in doc.Pages)
            {
                // Display each page for 5 seconds during presentation mode
                page.Duration = 5;
            }

            // Apply a transition effect to all pages using PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Process all pages (1‑based indexing)
                int[] allPages = new int[doc.Pages.Count];
                for (int i = 0; i < allPages.Length; i++)
                    allPages[i] = i + 1;

                editor.ProcessPages = allPages;
                // Fade transition – use the integer value that represents the enum (11)
                editor.TransitionType = 11; // Fade
                // Transition duration in seconds
                editor.TransitionDuration = 2;
                // Persist the changes to the output PDF
                editor.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF with page transitions saved to '{pdfPath}'.");
    }
}
