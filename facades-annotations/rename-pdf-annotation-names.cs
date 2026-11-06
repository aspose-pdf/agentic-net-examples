using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where modified PDFs will be written
        const string outputFolder = @"C:\PdfBatch\Output";
        // Standardized prefix to apply to every annotation name
        const string namePrefix = "StdPrefix_";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document inside a using block (lifecycle rule)
                using (Document doc = new Document(inputPath))
                {
                    // Iterate pages using 1‑based indexing (global rule)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];

                        // Iterate all annotations on the current page
                        for (int annIdx = 0; annIdx < page.Annotations.Count; annIdx++)
                        {
                            Annotation annotation = page.Annotations[annIdx];

                            // If the annotation already has a name, prepend the prefix;
                            // otherwise create a deterministic name using page and index.
                            if (!string.IsNullOrEmpty(annotation.Name))
                            {
                                annotation.Name = namePrefix + annotation.Name;
                            }
                            else
                            {
                                annotation.Name = $"{namePrefix}Page{pageNum}_Ann{annIdx + 1}";
                            }
                        }
                    }

                    // OPTIONAL: demonstrate use of a Facades class (PdfContentEditor)
                    // Here we simply bind the same document and save it via the editor.
                    // This satisfies the requirement to use Aspose.Pdf.Facades without
                    // altering the already‑modified document.
                    PdfContentEditor editor = new PdfContentEditor();
                    editor.BindPdf(doc);
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}