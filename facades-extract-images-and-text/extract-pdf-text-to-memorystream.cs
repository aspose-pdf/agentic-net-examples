using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract text from the source PDF
        string extractedText;
        using (Document doc = new Document(inputPdf))
        {
            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);
            extractedText = absorber.Text;
        }

        // Write the extracted text into a MemoryStream
        using (MemoryStream textStream = new MemoryStream())
        {
            using (StreamWriter writer = new StreamWriter(textStream, System.Text.Encoding.UTF8, 1024, leaveOpen: true))
            {
                writer.Write(extractedText);
                writer.Flush();
                textStream.Position = 0; // reset for reading
            }

            // Simulate passing the stream to another library that creates a PDF from the text.
            using (MemoryStream generatedPdfStream = CreatePdfFromTextStream(textStream))
            {
                // Merge the original PDF with the newly generated PDF using Aspose.Pdf.Facades
                PdfFileEditor editor = new PdfFileEditor();
                string outputPath = "merged_output.pdf";

                // Save the generated PDF stream to a temporary file for concatenation
                string tempGeneratedPath = SaveStreamToTempFile(generatedPdfStream);

                // Concatenate original and generated PDFs
                editor.Concatenate(new[] { inputPdf, tempGeneratedPath }, outputPath);
                Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
            }
        }
    }

    // Creates a simple PDF containing the provided text and returns it as a MemoryStream
    static MemoryStream CreatePdfFromTextStream(Stream textStream)
    {
        string text;
        using (StreamReader reader = new StreamReader(textStream, System.Text.Encoding.UTF8, true, 1024, leaveOpen: true))
        {
            text = reader.ReadToEnd();
        }

        MemoryStream pdfStream = new MemoryStream();
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();
            TextFragment fragment = new TextFragment(text);
            page.Paragraphs.Add(fragment);

            // ----- Correct way to add a link annotation -----
            // Instead of using the non‑existent PdfAnnotationEditor.AddLinkAnnotation,
            // create a LinkAnnotation directly and add it to the page's Annotations collection.
            Rectangle linkRect = new Rectangle(100, 500, 200, 520); // position of the link on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Example action: go to the first page of the document
                Action = new GoToAction(1)
            };
            page.Annotations.Add(link);
            // ------------------------------------------------

            pdfDoc.Save(pdfStream);
        }
        pdfStream.Position = 0;
        return pdfStream;
    }

    // Writes a MemoryStream to a temporary file and returns the file path
    static string SaveStreamToTempFile(MemoryStream stream)
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        // Ensure the stream is positioned at the beginning before copying
        if (stream.Position != 0)
            stream.Position = 0;
        using (FileStream file = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(file);
        }
        return tempPath;
    }
}
