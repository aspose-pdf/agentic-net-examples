using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExportPopupAnnotations
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXfdf = "popups.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdf))
        {
            // Create a new empty PDF that will hold only popup annotations
            using (Document tempDoc = new Document())
            {
                // Ensure the temporary document has the same number of pages as the source
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    // Add a blank page (size will be taken from the source page)
                    Page srcPage = srcDoc.Pages[i];
                    Page newPage = tempDoc.Pages.Add();
                    newPage.PageInfo = srcPage.PageInfo; // copy page size and rotation
                }

                // Iterate through each page and copy only PopupAnnotation objects
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    Page srcPage = srcDoc.Pages[i];
                    Page destPage = tempDoc.Pages[i];

                    for (int j = 1; j <= srcPage.Annotations.Count; j++)
                    {
                        Annotation ann = srcPage.Annotations[j];
                        if (ann is PopupAnnotation popup)
                        {
                            // Create a new popup annotation on the corresponding page
                            PopupAnnotation newPopup = new PopupAnnotation(destPage, popup.Rect)
                            {
                                Contents = popup.Contents,
                                Color    = popup.Color,
                                Open     = popup.Open,
                                Name     = popup.Name,
                                Modified = popup.Modified,
                                // Copy any other needed properties here
                            };

                            // Add the new popup annotation to the destination page
                            destPage.Annotations.Add(newPopup);
                        }
                    }
                }

                // Export only the popup annotations to XFDF
                tempDoc.ExportAnnotationsToXfdf(outputXfdf);
            }
        }

        Console.WriteLine($"Popup annotations exported to '{outputXfdf}'.");
    }
}