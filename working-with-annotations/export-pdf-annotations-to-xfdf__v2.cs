using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExportAnnotationsExample
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "annotations.xfdf";

        // Ensure the source PDF exists; if it does not, create a minimal PDF with a sample annotation.
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdfWithAnnotation(inputPdfPath);
        }

        // Load the PDF document. Document implements IDisposable, so we wrap it in a using block.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // MemoryStream will hold the XFDF data.
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations from the document into the stream.
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset the stream position before reading or copying.
                xfdfStream.Position = 0;

                // Optional: write the XFDF stream to a physical file.
                using (FileStream file = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(file);
                }

                // For demonstration purposes, output the XFDF content to the console.
                xfdfStream.Position = 0; // rewind again for reading
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine("XFDF content:\n" + xfdfContent);
                }
            }
        }

        Console.WriteLine("Annotation export completed.");
    }

    // Helper that creates a simple PDF containing a single text annotation.
    private static void CreateSamplePdfWithAnnotation(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Create a text annotation positioned on the page.
            TextAnnotation txtAnn = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
            {
                Title = "Sample",
                Contents = "This is a sample annotation."
            };
            page.Annotations.Add(txtAnn);
            doc.Save(path);
        }
    }
}