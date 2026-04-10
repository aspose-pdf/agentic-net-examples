using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDF files (relative to the executable directory)
        const string inputFolder = "Input";
        // Output folder for processed PDFs
        const string outputFolder = "Output";

        // Ensure both folders exist – creates them if they are missing.
        // This prevents DirectoryNotFoundException when the Input folder is absent.
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder. If none are found the loop simply does nothing.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{Path.GetFullPath(inputFolder)}'." );
            return;
        }

        // Process each PDF file in the input folder
        foreach (string inputPath in pdfFiles)
        {
            // Determine output file name (e.g., original name + "_annotated.pdf")
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_annotated.pdf");

            // Use PdfAnnotationEditor facade to load and save the document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            try
            {
                // Bind the PDF file to the facade
                editor.BindPdf(inputPath);

                // Access the underlying Document object
                Document doc = editor.Document;

                // Add a red rectangle annotation to every page
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Define rectangle coordinates (llx, lly, urx, ury)
                    // Here we place a 200x100 rectangle at (100,500)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                    // Create a square (rectangle) annotation
                    SquareAnnotation square = new SquareAnnotation(page, rect)
                    {
                        Color = Aspose.Pdf.Color.Red,   // Border color
                        Title = "Red Rectangle",
                        Contents = "Added by batch process"
                    };

                    // Add the annotation to the page
                    page.Annotations.Add(square);
                }

                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }
            finally
            {
                // Close the facade (releases the bound document)
                editor.Close();
            }

            Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
        }
    }
}
