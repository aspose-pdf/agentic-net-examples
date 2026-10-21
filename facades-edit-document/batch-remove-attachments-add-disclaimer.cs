using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchProcess
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        string outputFolder = "OutputPdfs";
        // Standard disclaimer text
        string disclaimer = "This document is confidential and intended solely for the recipient.";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string finalPath = Path.Combine(outputFolder, $"{fileName}_final.pdf");

            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // 1. Remove all embedded file attachments (if any)
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    // EmbeddedFileCollection uses 1‑based indexing and provides Delete(string name)
                    for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                    {
                        var fileSpec = doc.EmbeddedFiles[i];
                        if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        {
                            doc.EmbeddedFiles.Delete(fileSpec.Name);
                        }
                    }
                }

                // 2. Create a text annotation (disclaimer) on the first page
                // Rectangle: lower‑left (50,50), upper‑right (550,100) in points
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 50, 550, 100);
                var disclaimerAnnotation = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title = "Disclaimer",
                    Contents = disclaimer,
                    Open = true,
                    Icon = TextIcon.Note
                };

                // 3. Add the annotation to page 1 (if the document has at least one page)
                if (doc.Pages.Count > 0)
                {
                    doc.Pages[1].Annotations.Add(disclaimerAnnotation);
                }

                // 4. Save the processed PDF
                doc.Save(finalPath);
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
