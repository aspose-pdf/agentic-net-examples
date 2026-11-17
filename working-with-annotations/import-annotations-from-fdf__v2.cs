using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string fdfPath = "annotations.fdf";    // FDF containing annotations
        const string outputPath = "output.pdf";      // PDF with imported annotations

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the original PDF document.
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Load the FDF file. Aspose.Pdf can load an FDF directly as a Document
                // without requiring the separate Aspose.Pdf.Fdf assembly.
                using (Document fdfDoc = new Document(fdfPath))
                {
                    // Ensure both documents have the same page count or handle missing pages gracefully.
                    int pageCount = Math.Min(pdfDoc.Pages.Count, fdfDoc.Pages.Count);

                    // Pages are 1‑based in Aspose.Pdf.
                    for (int i = 1; i <= pageCount; i++)
                    {
                        Page targetPage = pdfDoc.Pages[i];
                        Page fdfPage = fdfDoc.Pages[i];

                        // Transfer each annotation from the FDF page to the corresponding PDF page.
                        foreach (Annotation srcAnn in fdfPage.Annotations)
                        {
                            // Clone the annotation to avoid reference issues.
                            Annotation clonedAnn = (Annotation)srcAnn.Clone();

                            // Add the cloned annotation to the target page.
                            targetPage.Annotations.Add(clonedAnn);
                        }
                    }
                }

                // Save the updated PDF.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
