using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF file names (generic for cross‑platform examples)
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDoc = new Document(inputPath);

            // Use the Facades API to edit annotations (cross‑platform)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(pdfDoc);

                // Work with the first page (1‑based indexing in Aspose.Pdf)
                Page firstPage = pdfDoc.Pages[1];
                for (int i = 1; i <= firstPage.Annotations.Count; i++)
                {
                    Annotation annot = firstPage.Annotations[i];

                    // Example: modify only SquareAnnotation instances
                    if (annot is SquareAnnotation squareAnnot)
                    {
                        // Change the annotation colour to red
                        squareAnnot.Color = Color.Red;

                        // Set a solid border of width 2
                        squareAnnot.Border = new Border(squareAnnot)
                        {
                            Style = BorderStyle.Solid,
                            Width = 2
                        };

                        // Update the visible text of the annotation
                        squareAnnot.Contents = "Modified annotation";
                    }
                }

                // Save the edited PDF – provide the destination path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
