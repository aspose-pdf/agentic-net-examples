using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Facades; // for FieldType enum

class PdfEditingDemo
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_edited.pdf";
        const string attachmentPath = "attachment.txt";
        const string stampImagePath = "stamp.png";
        const string imageToAddPath = "photo.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Load the PDF document (lifecycle rule)
        // -------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 2. Add a file attachment annotation (PdfContentEditor)
            // -------------------------------------------------
            using (PdfContentEditor contentEditor = new PdfContentEditor())
            {
                // Bind the document to the editor
                contentEditor.BindPdf(doc);

                // Annotation rectangle (System.Drawing.Rectangle) – 100x100 points at lower‑left corner
                var attachRect = new System.Drawing.Rectangle(50, 50, 100, 100);

                // Create the file attachment annotation on page 1
                contentEditor.CreateFileAttachment(
                    attachRect,
                    "Sample attachment",          // contents displayed in the annotation
                    attachmentPath,               // file to attach
                    1,                            // page number (1‑based)
                    "Paperclip");                 // icon name

                // -------------------------------------------------
                // 3. Add a rubber‑stamp annotation (PdfContentEditor)
                // -------------------------------------------------
                var stampRect = new System.Drawing.Rectangle(200, 200, 150, 100);
                contentEditor.CreateRubberStamp(
                    1,
                    stampRect,
                    "Approved",                   // annotation contents
                    System.Drawing.Color.Green,   // stamp color (System.Drawing.Color required)
                    stampImagePath);              // appearance file (image of the stamp)

                // Persist changes made by the content editor
                contentEditor.Save(outputPath);
            }

            // -------------------------------------------------
            // 4. Modify an existing annotation (PdfAnnotationEditor)
            // -------------------------------------------------
            using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
            {
                annotationEditor.BindPdf(outputPath); // work on the file just saved above

                // Create a new text annotation that will replace the first annotation on page 1
                var modRect = new System.Drawing.Rectangle(300, 300, 120, 80);
                var newAnnotation = new Aspose.Pdf.Annotations.TextAnnotation(
                    annotationEditor.Document.Pages[1],
                    new Aspose.Pdf.Rectangle(300, 300, 420, 380)) // Aspose.Pdf.Rectangle for bounds
                {
                    Title = "Updated Note",
                    Contents = "This annotation has been updated via PdfAnnotationEditor.",
                    Color = Aspose.Pdf.Color.Orange
                };

                // Annotations collection is 1‑based; replace the first annotation
                annotationEditor.ModifyAnnotations(1, 1, newAnnotation);
                annotationEditor.Save(outputPath);
            }

            // -------------------------------------------------
            // 5. Extract text and images (PdfExtractor)
            // -------------------------------------------------
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(outputPath);
                extractor.StartPage = 1;
                extractor.EndPage = doc.Pages.Count;

                // Extract all text to a .txt file
                extractor.ExtractText();
                string extractedTextPath = "extracted_text.txt";
                extractor.GetText(extractedTextPath);

                // Extract images – each image saved as PNG
                extractor.ExtractImage();
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imgPath = $"extracted_image_{imageIndex}.png";
                    extractor.GetNextImage(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                    imageIndex++;
                }
            }

            // -------------------------------------------------
            // 6. Add a new form field (FormEditor)
            // -------------------------------------------------
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(outputPath);

                // Add a text field on page 1 at position (100,500)-(250,550)
                formEditor.AddField(
                    FieldType.Text,
                    "NewTextField",
                    1,
                    100f, 500f, 250f, 550f);

                // Optionally set a default value for the field
                formEditor.SetFieldAttribute("NewTextField", PropertyFlag.ReadOnly);
                formEditor.Save(outputPath);
            }

            // -------------------------------------------------
            // 7. Add an image to the document (PdfFileMend)
            // -------------------------------------------------
            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf(outputPath);

                // Add the image to page 1 at coordinates (50,700)-(250,900)
                mend.AddImage(
                    imageToAddPath,
                    new int[] { 1 },          // pages array (single page)
                    50f, 700f, 250f, 900f);   // llx, lly, urx, ury

                mend.Save(outputPath);
            }

            // -------------------------------------------------
            // 8. Final save (document lifecycle rule)
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF editing completed. Output saved to '{outputPath}'.");
    }
}