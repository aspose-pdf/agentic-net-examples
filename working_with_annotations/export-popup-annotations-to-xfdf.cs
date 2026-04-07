using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExportPopupAnnotations
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputXfdf = "popups.xfdf";       // XFDF file to create

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through each page and remove all annotations except PopupAnnotation
            foreach (Page page in doc.Pages)
            {
                // Delete annotations in reverse order to keep indexes valid
                for (int idx = page.Annotations.Count; idx >= 1; idx--)
                {
                    Annotation ann = page.Annotations[idx];
                    if (!(ann is PopupAnnotation))
                    {
                        page.Annotations.Delete(idx);
                    }
                }
            }

            // Export the remaining (popup) annotations to XFDF
            doc.ExportAnnotationsToXfdf(outputXfdf);
        }

        Console.WriteLine($"Popup annotations exported to '{outputXfdf}'.");
    }
}