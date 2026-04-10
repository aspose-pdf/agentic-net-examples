using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchProcessPdf
{
    static void Main()
    {
        const string inputDirectory = @"C:\InputPdfs";
        const string outputDirectory = @"C:\OutputPdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        foreach (string inputFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // ---------- Step 1: Remove all attachments ----------
                string tempFilePath = Path.Combine(outputDirectory,
                    Path.GetFileNameWithoutExtension(inputFilePath) + "_noattach.pdf");

                PdfContentEditor contentEditor = new PdfContentEditor();
                contentEditor.BindPdf(inputFilePath);
                contentEditor.DeleteAttachments();
                contentEditor.Save(tempFilePath);
                contentEditor.Close(); // optional, releases resources

                // ---------- Step 2: Add standardized disclaimer annotation ----------
                using (Document doc = new Document(tempFilePath))
                {
                    // Use the first page (1‑based indexing)
                    Aspose.Pdf.Page page = doc.Pages[1];

                    // Define the rectangle where the disclaimer will appear
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 50, 550, 100);

                    // DefaultAppearance requires System.Drawing.Color for the text color
                    DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Gray);

                    // Create the free‑text annotation with the disclaimer text
                    FreeTextAnnotation disclaimer = new FreeTextAnnotation(page, rect, appearance)
                    {
                        Contents = "This document is confidential and intended solely for the recipient.",
                        Color = Aspose.Pdf.Color.LightGray // background color of the annotation box
                        // The 'Open' property does not exist on FreeTextAnnotation in current API versions
                    };

                    // Add the annotation to the page
                    page.Annotations.Add(disclaimer);

                    // Save the final PDF (overwrites the temporary file)
                    string finalPath = Path.Combine(outputDirectory, Path.GetFileName(inputFilePath));
                    doc.Save(finalPath);
                }

                // Optionally delete the intermediate temporary file
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputFilePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFilePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
