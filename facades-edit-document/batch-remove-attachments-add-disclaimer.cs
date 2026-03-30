using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputFolder = "input_pdfs";
        const string disclaimerText = "This document is confidential and intended for the designated recipient only.";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine("Input folder not found: " + inputFolder);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Remove all embedded files (attachments)
                    if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                    {
                        // EmbeddedFileCollection is 1‑based and does not expose RemoveAt.
                        // Delete each file by its name.
                        while (doc.EmbeddedFiles.Count > 0)
                        {
                            // Get the first embedded file (index 1)
                            FileSpecification spec = doc.EmbeddedFiles[1];
                            // Delete it using its Name property
                            doc.EmbeddedFiles.Delete(spec.Name);
                        }
                    }

                    // Add a disclaimer text annotation on the first page
                    if (doc.Pages.Count > 0)
                    {
                        Page firstPage = doc.Pages[1];
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);
                        TextAnnotation disclaimer = new TextAnnotation(firstPage, rect)
                        {
                            Title = "Disclaimer",
                            Contents = disclaimerText,
                            Open = true,
                            Icon = TextIcon.Note
                        };
                        firstPage.Annotations.Add(disclaimer);
                    }

                    // Overwrite the original file (use full path)
                    doc.Save(pdfPath);
                }

                Console.WriteLine("Processed: " + fileName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
