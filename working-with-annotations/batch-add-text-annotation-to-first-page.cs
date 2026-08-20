using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAddTextAnnotation
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\PdfFolder";
        // Folder where annotated PDFs will be saved (can be the same as inputFolder)
        const string outputFolder = @"C:\PdfFolder\Annotated";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Standard annotation properties
        const string annotationTitle = "Note";
        const string annotationContents = "Standard annotation added to first page.";
        // Rectangle coordinates: left, bottom, right, top (points)
        const double llx = 100; // left
        const double lly = 700; // bottom
        const double urx = 300; // right
        const double ury = 750; // top

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count < 1)
                    {
                        Console.WriteLine($"Skipping '{pdfPath}': no pages found.");
                        continue;
                    }

                    // Get the first page (1‑based indexing)
                    Page firstPage = doc.Pages[1];

                    // Create a fully qualified rectangle for the annotation bounds
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Create the text annotation and set its properties
                    TextAnnotation textAnn = new TextAnnotation(firstPage, rect)
                    {
                        Title = annotationTitle,
                        Contents = annotationContents,
                        Color = Aspose.Pdf.Color.Yellow, // background color of the annotation
                        Open = true,                     // annotation is opened by default
                        Icon = TextIcon.Note             // standard note icon
                    };

                    // Add the annotation to the page's annotation collection
                    firstPage.Annotations.Add(textAnn);

                    // Build output file path (original name with suffix)
                    string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_annotated.pdf");

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Annotated PDF saved: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}