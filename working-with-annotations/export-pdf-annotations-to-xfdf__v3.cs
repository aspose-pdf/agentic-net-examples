using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF with an annotation entirely in memory
        using (var pdfStream = new MemoryStream())
        {
            // Build the PDF document
            using (var doc = new Document())
            {
                var page = doc.Pages.Add();

                // Add a simple text annotation
                var annotation = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
                {
                    Title = "Sample",
                    Subject = "Demo",
                    Contents = "This is a sample annotation."
                };
                page.Annotations.Add(annotation);

                // Save the PDF into the memory stream (no file system access)
                doc.Save(pdfStream);
            }

            // Reset the stream position so it can be read again
            pdfStream.Position = 0;

            // Load the PDF from the memory stream
            using (var pdfDoc = new Document(pdfStream))
            {
                // Prepare a memory stream to hold the XFDF data
                using (var xfdfStream = new MemoryStream())
                {
                    // Export all annotations to XFDF (in‑memory)
                    pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
                    xfdfStream.Position = 0;

                    // Optional: write the XFDF data to a file for inspection
                    using (var file = new FileStream("annotations.xfdf", FileMode.Create, FileAccess.Write))
                    {
                        xfdfStream.CopyTo(file);
                    }

                    // Optional: display XFDF content on the console
                    xfdfStream.Position = 0;
                    using (var reader = new StreamReader(xfdfStream))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }
        }
    }
}
