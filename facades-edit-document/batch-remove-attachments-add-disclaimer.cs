using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchProcessPdf
{
    // Standard disclaimer text
    private const string DisclaimerTitle = "Disclaimer";
    private const string DisclaimerContent = "This document is confidential and intended solely for the designated recipient.";

    static void Main()
    {
        // Input directory containing PDFs to process
        string inputDirectory = @"C:\InputPdfs";
        // Output directory where processed PDFs will be saved
        string outputDirectory = @"C:\ProcessedPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Step 1: Remove all attachments using PdfContentEditor
                string tempPath = Path.Combine(outputDirectory,
                    Path.GetFileNameWithoutExtension(pdfFilePath) + "_noattach.pdf");

                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(pdfFilePath);
                editor.DeleteAttachments();
                editor.Save(tempPath);

                // Step 2: Add disclaimer annotation to each page
                string finalPath = Path.Combine(outputDirectory,
                    Path.GetFileNameWithoutExtension(pdfFilePath) + "_final.pdf");

                using (Document doc = new Document(tempPath))
                {
                    // Iterate pages (1‑based indexing)
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        // Define annotation rectangle (left, bottom, right, top)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 50, 550, 100);

                        // Create a TextAnnotation with the disclaimer
                        TextAnnotation disclaimer = new TextAnnotation(page, rect)
                        {
                            Title = DisclaimerTitle,
                            Contents = DisclaimerContent,
                            Color = Aspose.Pdf.Color.Yellow, // Background color of the annotation
                            Open = true,
                            Icon = TextIcon.Note
                        };

                        // Add the annotation to the page
                        page.Annotations.Add(disclaimer);
                    }

                    // Save the final PDF (overwrites the temporary file)
                    doc.Save(finalPath);
                }

                // Optional: delete the intermediate file
                File.Delete(tempPath);
                Console.WriteLine($"Processed: {Path.GetFileName(pdfFilePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFilePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}