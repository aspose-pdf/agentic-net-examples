using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the Markdown file to be rendered.
        const string markdownPath = "sample.md";

        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"File not found: {markdownPath}");
            return;
        }

        // Load the Markdown file and convert it to a PDF document.
        // MdLoadOptions tells Aspose.Pdf how to interpret the .md source.
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Document implements IDisposable – wrap in using for deterministic cleanup.
        using (Document pdfDoc = new Document(markdownPath, mdLoadOptions))
        {
            // PdfViewer is a facade that can work with an existing Document.
            // It also implements IDisposable, so we use a using block.
            using (PdfViewer viewer = new PdfViewer())
            {
                // Bind the PDF document to the viewer facade.
                viewer.BindPdf(pdfDoc);

                // Extract the textual content of the PDF.
                // TextAbsorber works on the Document, not directly on the viewer.
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages.Accept(absorber);

                // Output the extracted text to the console.
                Console.WriteLine(absorber.Text);

                // No explicit Save is required – we only needed the viewer to demonstrate
                // the use of a facade as per the task description.
            }
        }
    }
}