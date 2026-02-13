using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Edit annotations using PdfAnnotationEditor (Facades API)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the editor to the loaded document
                editor.BindPdf(pdfDoc);

                // Iterate over all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Process each annotation on the page
                    foreach (Annotation annot in page.Annotations)
                    {
                        // Example: modify LinkAnnotation appearance
                        if (annot is LinkAnnotation link)
                        {
                            // Change the annotation color to red (Aspose.Pdf.Color is cross‑platform)
                            link.Color = Color.Red;

                            // Initialize the border after the annotation object is created
                            link.Border = new Border(link)
                            {
                                Style = BorderStyle.Solid,
                                Width = 1
                            };
                        }
                    }
                }

                // No need to call editor.Save() without a path; changes are applied to the bound Document.
            }

            // Save the edited PDF document
            pdfDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
